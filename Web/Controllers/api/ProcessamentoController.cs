using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ProcessamentoController : BaseController
    {
        public ProcessamentoController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor) { }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("VisualizarTodasEntidades,VisualizarMinhasEntidades", "True")]
        public IActionResult ListarPorFiltro([FromForm] ProcessamentoListarFiltroViewModel viewModel)
        {
            if (!ModelState.IsValid)
                AdicionarResultado(ref resultadoSelecao);
            else
            {
                if (!viewModel.AmbienteId.HasValue)
                    viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

                if (!usuarioLogado.PossuiClaim("VisualizarTodasEntidades") && !usuarioLogado.PertenceAoAmbiente(viewModel.AmbienteId.Value))
                    resultadoSelecao.Mensagem = "Acesso negado.";
                else
                {
                    var ambiente = _aplicacao.Ambiente.SelecionarPorId(viewModel.AmbienteId.Value);
                    if (!string.IsNullOrEmpty(viewModel.ProjetoId) && !ambiente.Projetos.Any(x => x.Id == viewModel.ProjetoId))
                        resultadoSelecao.Mensagem = "O projeto Id informado não foi localizado";
                    else
                    {
                        DateTime? dataDe = null;
                        DateTime? dataAte = null;

                        if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataDeT))
                            dataDe = dataDeT;

                        if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataAteT))
                            dataAte = dataAteT;

                        switch (viewModel.ProcessamentoTipoId)
                        {
                            case (int)ProcessamentoTipoEnum.Indexer:
                                resultadoSelecao = _aplicacao.ProcessamentoIndexer.SelecionarPorFiltro(viewModel.Id, viewModel.AmbienteId, viewModel.ProjetoId, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
                                break;                            
                            case (int)ProcessamentoTipoEnum.Anexo:
                                resultadoSelecao = _aplicacao.ProcessamentoAnexo.SelecionarPorFiltro(viewModel.Id, viewModel.AmbienteId, viewModel.ProjetoId, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
                                break;
                            case (int)ProcessamentoTipoEnum.Ner:
                                resultadoSelecao = _aplicacao.ProcessamentoNer.SelecionarPorFiltro(viewModel.Id, viewModel.AmbienteId, viewModel.ProjetoId, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
                                break;                                                            
                            default:
                                resultadoSelecao.Mensagem = "O tipo de processamento selecionado não existe.";
                                break;
                        }                        
                    }
                }
            }

            return AdicionarResultado(ref resultadoSelecao);
        }      

    }
}
