using Dominio.Entidades;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : BaseController
    {
        public UsuarioController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor) { }

        [HttpGet]
        public IActionResult AlterarDarkMode(bool ativar)
        {
            if (usuarioLogado.TemaConfiguracao == null)
                usuarioLogado.TemaConfiguracao = new TemaConfiguracao();

            usuarioLogado.TemaConfiguracao.DarkMode = ativar;
            _aplicacao.Usuario.Atualizar(usuarioLogado);
            _aplicacao.SaveChanges();

            resultado.Sucesso = true;
            resultado.Retorno = usuarioLogado.TemaConfiguracao.SerializarParaAtualizar();

            return Content(GerarRetorno(resultado), "application/json");
        }
        [HttpGet]
        public IActionResult AlterarTema(string skin, int tipo)
        {
            if (usuarioLogado.TemaConfiguracao == null)
                usuarioLogado.TemaConfiguracao = new TemaConfiguracao();

            switch ((AlteracaoCoresTipoEnum)tipo)
            {
                case AlteracaoCoresTipoEnum.BarraSuperior:
                    usuarioLogado.TemaConfiguracao.LogoBg = skin;
                    usuarioLogado.TemaConfiguracao.NavbarBg = skin;
                    break;
                case AlteracaoCoresTipoEnum.BarraLateral:
                    usuarioLogado.TemaConfiguracao.SidebarColor = skin;
                    break;
                case AlteracaoCoresTipoEnum.Componentes:
                    usuarioLogado.TemaConfiguracao.Components = skin;
                    break;
                default:
                    break;
            }

            _aplicacao.Usuario.Atualizar(usuarioLogado);
            _aplicacao.SaveChanges();

            resultado.Sucesso = true;
            resultado.Retorno = usuarioLogado.TemaConfiguracao.SerializarParaAtualizar();

            return Content(GerarRetorno(resultado), "application/json");
        }

        [HttpPost]
        [ClaimsAuthorize("AlterarMeusUsuarios,AlterarTodosUsuarios", "True")]
        public async Task<ActionResult> Salvar([FromForm] UsuarioViewModel viewModel)
        {
            var resultado = new Resultado<object>();
            var ambiente = _aplicacao.Ambiente.SelecionarFirstOrDefault(og => og.Id == viewModel.AmbienteSelecionadoId);

            if (ambiente == null || (!usuarioLogado.PossuiClaims("AlterarTodosUsuarios") && !usuarioLogado.PertenceAoAmbiente(ambiente.Id)))
                resultado.Mensagem = "Acesso negado";
            else if (!ModelState.IsValid)
                AdicionarErrosModelStateResultado(ref resultado);
            else
            {
                var papel = usuarioLogado.Papel.PapeisAcessiveis.FirstOrDefault(p => p.PapelId == viewModel.PapelSelecionadoId);

                if (papel == null)
                    resultado.Mensagem = "Perfil inválido";
                else
                {
                    var resultadoUsuario = await _aplicacao.Usuario.Salvar(ambiente, usuarioLogado, papel.Papel, true, viewModel.Email, viewModel.Nome);
                    resultado.Mensagem = resultadoUsuario.Mensagem;

                    if (resultadoUsuario.Sucesso)
                    {
                        _aplicacao.SaveChanges();
                        resultado.Sucesso = true;
                    }
                }
            }

            return Content(GerarRetorno(resultado), "application/json");
        }

        [ClaimsAuthorize("VisualizarTodosUsuarios,VisualizarMeusUsuarios", "True")]
        public ActionResult ListarFiltro([FromForm] UsuarioListaViewModel viewModel)
        {
            var ambientesIds = new List<int>();

            if (viewModel.AmbienteSelecionadoId.HasValue && !usuarioLogado.PossuiClaims("VisualizarTodosUsuarios") && !usuarioLogado.PertenceAoAmbiente(viewModel.AmbienteSelecionadoId.Value))
                resultadoSelecao.Mensagem = "Acesso negado.";
            else
            {
                if (!usuarioLogado.PossuiClaims("VisualizarTodosUsuarios"))
                    ambientesIds.Add(usuarioLogado.Ambiente.Id);
                else if (viewModel.AmbienteSelecionadoId.HasValue)
                    ambientesIds.Add(viewModel.AmbienteSelecionadoId.Value);
                else
                    ambientesIds.AddRange(_aplicacao.Ambiente.SelecionarTodos().ToList().Select(a => a.Id));

                resultadoSelecao = _aplicacao.Usuario.SelecionarUsuariosPorFiltro(ambientesIds.ToArray(), viewModel.Nome, viewModel.UserName, null, viewModel.start, viewModel.length);
            }

            return Content(GerarRetorno(resultadoSelecao), "application/json");
        }
    }
}
