using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class EntidadeController : BaseController
    {
        public EntidadeController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodasEntidades,AlterarMinhasEntidades", "True")]
        public IActionResult Cadastrar([FromForm] EntidadeCadastroViewModel viewModel)
        {
            var resultado = new Resultado<object>();

            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                var documentoTipo = _aplicacao.DocumentoTipo.SelecionarPorId(viewModel.DocumentoTipoId);
                if (documentoTipo == null)
                    resultado.Mensagem = "O tipo de documento informado é inválido.";
                else if (!usuarioLogado.PertenceAoAmbiente(documentoTipo.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodasEntidades"))
                    resultado.Mensagem = "A entidade não pode ser cadastrada em ambiente difrentes do usuários logado.";
                else if (documentoTipo.Ambiente.NumeroEntidades == documentoTipo.Ambiente.NumeroEntidadesCadastradas())
                    resultado.Mensagem = "Você atingiu o limite de criações de entidades, remova alguma ou contate um administrador para realizar um upgrade em seu contrato.";
                else
                    resultado = _aplicacao.Entidade.Cadastrar(viewModel.Nome, viewModel.Pergunta, viewModel.DocumentoTipoId, usuarioLogado);
            }

            if (resultado.Sucesso)
                _aplicacao.SaveChanges();

            return AdicionarResultado(ref resultado);
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodasEntidades,AlterarMinhasEntidades", "True")]
        public IActionResult Excluir(ExcluirRegistroViewModel viewModel)
        {
            var resultado = new Resultado<object>();
            var entidade = _aplicacao.Entidade.SelecionarPorId(viewModel.Id);

            if (entidade == null || (entidade != null && !usuarioLogado.PertenceAoAmbiente(entidade.DocumentoTipo.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodasEntidades")))
                resultado.Mensagem = "A entidade solicitado não pertence a rede do usuário logado";
            else if (!viewModel.Excluir && entidade.DocumentoTipo.Ambiente.NumeroEntidades == entidade.DocumentoTipo.Ambiente.NumeroEntidadesCadastradas())
                resultado.Mensagem = "Você atingiu o limite de criações de entidades, remova alguma ou contate um administrador para realizar um upgrade em seu contrato.";
            else
            {
                entidade.Excluido = viewModel.Excluir;
                entidade.AtribuirInformacoesRegistroParaAlteracao(usuarioLogado.Id);
                _aplicacao.Entidade.Atualizar(entidade);
                _aplicacao.SaveChanges();
                resultado.Sucesso = true;
            }


            return AdicionarResultado(ref resultado);
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("VisualizarTodasEntidades,VisualizarMinhasEntidades", "True")]
        public IActionResult ListarPorFiltro([FromForm] EntidadeListarViewModel viewModel)
        {
            if (!viewModel.AmbienteId.HasValue)
                viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

            if (!usuarioLogado.PossuiClaim("VisualizarTodasEntidades") && !usuarioLogado.PertenceAoAmbiente(viewModel.AmbienteId.Value))
                resultadoSelecao.Mensagem = "Acesso negado.";
            else
            {
                DateTime? dataDe = null;
                DateTime? dataAte = null;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataDeT))
                    dataDe = dataDeT;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataAteT))
                    dataAte = dataAteT;

                resultadoSelecao = _aplicacao.Entidade.SelecionarPorFiltro(viewModel.AmbienteId, viewModel.DocumentoTipoId, viewModel.Nome, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
            }

            return AdicionarResultado(ref resultadoSelecao);
        }
    }
}
