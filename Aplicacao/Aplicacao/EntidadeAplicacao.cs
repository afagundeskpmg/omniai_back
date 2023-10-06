using Aplicacao.Interface;
using Aplicacao.Util.OpenAI;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class EntidadeAplicacao : BaseAplicacao<Entidade>, IEntidadeAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        public EntidadeAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
        }

        public Resultado<object> Cadastrar(string nome, string pergunta, string documentoTipoId, Usuario usuario)
        {
            var resultado = new Resultado<object>();
            var documentoTipo = _repositorio.DocumentoTipo.SelecionarPorId(documentoTipoId);

            if (string.IsNullOrEmpty(nome))
                resultado.Mensagem = "O nome não pode ser nulo ou vazio";
            else if (string.IsNullOrEmpty(pergunta))
                resultado.Mensagem = "O propriedade pergunta não pode ser nulo ou vazio";
            else if (documentoTipo == null)
                resultado.Mensagem = "O tipo de documento informado não foi localizado.";            
            else if (!usuario.Ambientes.Any(x => x.Id == documentoTipo.AmbienteId) && !usuario.Claims.Any(p => p.ClaimType == "AlterarTodasEntidades"))
                resultado.Mensagem = "O tipo de documento informado não pertence a rede deste usuário";
            else
            {
                var entidade = new Entidade(nome, pergunta,documentoTipoId, usuario.Id);
                resultado = GerarQueryConsulta(ref entidade);

                if (resultado.Sucesso)
                {
                    Inserir(entidade);
                    resultado.Retorno = entidade.SerializarParaListar();
                    resultado.Sucesso = true;
                }
            }

            return resultado;
        }

        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? documentoTipoId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido ,int? start, int? length)
        {
            return _repositorio.Entidade.SelecionarPorFiltro(ambienteId, documentoTipoId, nome, dataDe, dataAte, excluido, start, length);
        }        
        private Resultado<object> GerarQueryConsulta(ref Entidade entidade) 
        {
            #region Parametros
            var resultado = new Resultado<object>() {};
            float temperatureOut = 0;
            int maxTokenOut = 0;
            var prompt = "Abaixo está uma pergunta do usuário que precisa ser respondida.\nGerar um comando de busca.\nNão inclua citação de nome de arquivos ou nomes de documentos e.g info.txt ou doc.pdf nos termos de comando de busca.\nNão inclua nenhum texto dentro [] ou <<>> nos termos de comando de busca.\n\nPergunta:\n{0}\n\nComando de busca:\n";
            var temperatureString = SelecionarParametro(ParametroAmbienteEnum.AzureOpenAITemperatureQueryGen);
            var maxTokenString = SelecionarParametro(ParametroAmbienteEnum.AzureOpenAIMaxTokenQueryGen);
            float.TryParse(temperatureString, out temperatureOut);
            int.TryParse(maxTokenString, out maxTokenOut);
            prompt = string.Format(prompt, entidade.Pergunta);
            #endregion

            var resultadoRequisicao = FazerPerguntaChatGPT(prompt, temperatureOut, maxTokenOut);
            if (!resultadoRequisicao.Sucesso)
                resultado.Mensagem = resultadoRequisicao.Mensagem;
            else
            {
                entidade.Query = resultadoRequisicao.Retorno.Query;
                entidade.Dados = resultadoRequisicao.Retorno.Json;
                resultado.Sucesso = true;
            }

            return resultado;
        }         
    }
}
