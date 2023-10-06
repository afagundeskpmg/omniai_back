using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class DocumentoTipoController : BaseController
    {
        public DocumentoTipoController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosDocumentosTipo,AlterarMeusDocumentosTipo", "True")]
        public IActionResult Cadastrar([FromForm] DocumentoTipoCadastroViewModel viewModel)
        {
            var resultado = new Resultado<object>();

            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                if (!viewModel.AmbienteId.HasValue)
                    viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

                resultado = _aplicacao.DocumentoTipo.Cadastrar(viewModel.Nome, (int)viewModel.AmbienteId, usuarioLogado);
            }

            if (resultado.Sucesso)
                _aplicacao.SaveChanges();

            return AdicionarResultado(ref resultado);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosDocumentosTipo,VisualizarMeusDocumentosTipo", "True")]
        public IActionResult ListarPorFiltro([FromForm] DocumentoTipoListarViewModel viewModel)
        {
            if (!viewModel.AmbienteId.HasValue)
                viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

            if (!usuarioLogado.PossuiClaim("VisualizarTodosDocumentosTipo") && !usuarioLogado.PertenceAoAmbiente(viewModel.AmbienteId.Value))
                resultadoSelecao.Mensagem = "Acesso negado.";
            else
            {
                DateTime? dataDe = null;
                DateTime? dataAte = null;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataDeT))
                    dataDe = dataDeT;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataAteT))
                    dataAte = dataAteT;

                resultadoSelecao = _aplicacao.DocumentoTipo.SelecionarPorFiltro(viewModel.AmbienteId, viewModel.Nome, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
            }

            return AdicionarResultado(ref resultadoSelecao);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosDocumentosTipo,AlterarMeusDocumentosTipo", "True")]
        public IActionResult Excluir(ExcluirRegistroViewModel viewModel)
        {
            var resultado = new Resultado<object>();
            var documentoTipo = _aplicacao.DocumentoTipo.SelecionarPorId(viewModel.Id);

            if (documentoTipo == null || (documentoTipo != null && !usuarioLogado.PertenceAoAmbiente(documentoTipo.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosDocumentos")))
                resultado.Mensagem = "O tipo de documento solicitado não pertence a rede do usuário logado";
            else
            {
                documentoTipo.Excluido = viewModel.Excluir;
                documentoTipo.AtribuirInformacoesRegistroParaAlteracao(usuarioLogado.Id);

                _aplicacao.DocumentoTipo.Atualizar(documentoTipo);
                _aplicacao.SaveChanges();
                resultado.Sucesso = true;
            }


            return AdicionarResultado(ref resultado);
        }
    }
}
