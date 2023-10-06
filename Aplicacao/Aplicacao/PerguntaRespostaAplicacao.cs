using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Aplicacao
{
    public class PerguntaRespostaAplicacao : BaseAplicacao<PerguntaResposta>, IPerguntaRespostaAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        public PerguntaRespostaAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
        }      
        public Resultado<PerguntaResposta> Perguntar(PerguntaResposta perguntaResposta)
        {
            var resultado = new Resultado<PerguntaResposta>() { Retorno = perguntaResposta };
            var resultadoComposicaoPrompt = ComporPrompParaConsultaChatGPT(ref perguntaResposta);
            var limiteTokenPorRequisicao = perguntaResposta.Projeto.Ambiente.LimiteTokenPorRequisicao;

            if (!resultadoComposicaoPrompt.Sucesso)
                resultado.AtribuirMensagemErro(resultadoComposicaoPrompt);
            else
            {
                resultado.Retorno.TokensPergunta = CalcularNumeroTokens(resultado.Retorno.Prompt);

                if (resultado.Retorno.TokensPergunta <= limiteTokenPorRequisicao)
                {
                    var resultadoPergunta = FazerPerguntaChatGPT(resultado.Retorno.Prompt, float.Parse("0,5"), 1024);

                    if (!resultadoPergunta.Sucesso)
                        resultado.AtribuirMensagemErro(resultadoPergunta);
                    else
                    {
                        resultado.Retorno.Resposta = resultadoPergunta.Retorno.Query;
                        resultado.Retorno.Dados = resultadoPergunta.Retorno.Json;

                        Atualizar(resultado.Retorno);                        
                        resultado.Sucesso = true;
                    }
                }
            }
            return resultado;
        }

    }
}
