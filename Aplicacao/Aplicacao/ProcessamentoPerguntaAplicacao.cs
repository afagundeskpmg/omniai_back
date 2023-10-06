using Aplicacao.Interface;
using Aplicacao.Util.OpenAI;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Aplicacao
{
    public class ProcessamentoPerguntaAplicacao : BaseAplicacao<ProcessamentoPergunta>, IProcessamentoPerguntaAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IPerguntaRespostaAplicacao _perguntaRespostaAplicacao;
        public ProcessamentoPerguntaAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao,IPerguntaRespostaAplicacao perguntaRespostaAplicacao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
            _perguntaRespostaAplicacao = perguntaRespostaAplicacao;
        }
        public void Processar(ref ProcessamentoPergunta processamento)
        {
            try
            {
                #region Atualizar Status Processamento
                processamento.AtualizarStatusDeProcessamento(ProcessamentoStatusEnum.EmProcessamento, null);
                Atualizar(processamento);
                _repositorio.SaveChanges();
                #endregion

                processamento.InicioConsulta = DateTime.Now;
                var resultadoPergunta = _perguntaRespostaAplicacao.Perguntar(processamento.PerguntaResposta);
                processamento.FimConsulta = DateTime.Now;

                if (!resultadoPergunta.Sucesso)
                    processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(string.Concat("Ocorreu um erro no processameto Ex:", resultadoPergunta.Mensagem)));
                else                
                    processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComSucesso, null, null);
               
            }
            catch (Exception ex)
            {
                var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(string.Concat("Ocorreu um erro no processameto Ex:", ex.Message)));
                Log.LogArquivo.Log(ex, processamento.Id.ToString());
            }                     

            Atualizar(processamento);
        }
    }
}
