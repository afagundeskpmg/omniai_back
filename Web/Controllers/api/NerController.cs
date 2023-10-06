using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class NerController : BaseController
    {
        public NerController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }
        [HttpGet]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("VisualizarTodasEntidades,VisualizarMinhasEntidades", "True")]
        public IActionResult ConsultarResultado(string ProcessamentoId)
        {
            var processamento = _aplicacao.ProcessamentoNer.SelecionarPorId(ProcessamentoId);
            if (processamento == null)
                resultado.Mensagem = "Processamento não localizado";
            else if (!usuarioLogado.PossuiClaim("VisualizarTodasProcessamentos") && !usuarioLogado.PertenceAoAmbiente(processamento.Projeto.AmbienteId))
                resultado.Mensagem = "O processamento solicitado não pertence ao ambiente do usuário logado.";
            else
            {
                resultado.Retorno = processamento.SerializarParaListar();
                resultado.Sucesso = true;
            }

            return AdicionarResultado(ref resultado);
        }
        [HttpGet]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosProjetos,AlterarMeusProjetos", "True")]
        public IActionResult Executar(string ProjetoId)
        {
            var projeto = _aplicacao.Projeto.SelecionarPorId(ProjetoId);

            if (projeto == null)
                resultado.Mensagem = "O projeto não foi localizado";
            else if (projeto != null && !usuarioLogado.PertenceAoAmbiente(projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos"))
                resultado.Mensagem = "O projeto não pertence ao ambiente do usuário logado";
            else if (!projeto.Anexos.Any(x => x.DocumentoTipo.Entidades != null && x.DocumentoTipo.Entidades.Any()))
                resultado.Mensagem = "O projeto selecionado não contem um anexos válido para executar o ner.";
            else if (!projeto.ProcessamentosIndexers.Any(x => !x.Excluido && x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComSucesso))
                resultado.Mensagem = "O projeto selecionado não contem um processamento válido para executar o ner.";
            else if (projeto.ProcessamentosNer.Any(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.EmProcessamento || x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessamentoSolicitado))
                resultado.Mensagem = "Este projeto ja possui uma solicitação de processamento Ner em andamento, aguarde a conclusão para solicitar um novo.";
            else
            {

                var processamentoStatus = _aplicacao.ProcessamentoStatus.SelecionarPorId((int)ProcessamentoStatusEnum.ProcessamentoSolicitado);
                Processamento processamento = new ProcessamentoNer(usuarioLogado.Id, ProcessamentoStatusEnum.ProcessamentoSolicitado);
                processamento.ProcessamentoStatus = processamentoStatus;
                projeto.ProcessamentosNer.Add((ProcessamentoNer)processamento);
                _aplicacao.ProcessamentoNer.Inserir((ProcessamentoNer)processamento);
                _aplicacao.SaveChanges();

                var processamentoTipo = _aplicacao.ProcessamentoTipo.SelecionarPorId((int)ProcessamentoTipoEnum.Ner);
                _aplicacao.Processamento.AtribuirQueueProcessamento(ref processamento, _aplicacao.Processamento.SelecionarParametro(ParametroAmbienteEnum.StorageConnection), processamentoTipo.QueueNome, JsonConvert.SerializeObject(new { Id = processamento.Id, projeto.Ambiente.Cliente.Pais.CultureInfo }), 0);

                _aplicacao.Processamento.Atualizar(processamento);
                _aplicacao.SaveChanges();

                resultado.Retorno = processamento.SerializarParaListar();
                resultado.Sucesso = true;

            }

            return AdicionarResultado(ref resultado);
        }


    }
}
