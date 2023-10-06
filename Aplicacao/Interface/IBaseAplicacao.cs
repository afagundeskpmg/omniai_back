using Aplicacao.Util.OpenAI;
using Azure.AI.OpenAI;
using Azure.Search.Documents.Indexes;
using Dominio.Entidades;
using RestSharp;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Aplicacao
{
    public interface IBaseAplicacao<TEntity> where TEntity : class
    {
        #region Dados
        void Inserir(TEntity obj);
        void InserirTodos(ICollection<TEntity> obj);
        void Atualizar(TEntity obj);
        void Recarregar(TEntity obj);
        void Deletar(TEntity obj);
        void DeletarTodos(ICollection<TEntity> obj);
        int ExecutarQuery(string query);
        TEntity SelecionarPorId(int id);
        TEntity SelecionarPorId(string id);
        IEnumerable<TEntity> SelecionarTodos(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool noTracking = false);
        TEntity SelecionarFirstOrDefault(Expression<Func<TEntity, bool>> filter = null);
        object ExecutarMetodo(string methodName, object[] parameters, Type[] parametersTypes);
        #endregion

        #region Utilitarios
        Task<RestResponse> FazerRequisicao(Method metodo, string url, List<Dominio.Entidades.Parameter> parametros, string? contentType);
        Task<RestResponse> FazerRequisicao(Method metodo, string url, string? corpo, string? contentType);
        string SelecionarParametro(ParametroAmbienteEnum parametroAmbienteEnum);
        Resultado<decimal> ConverterParaDecimal(string valorString, string mascara);
        Resultado<DateTime> ConverterParaDateTime(string valorString, string mascara);
        string DeterminarPastaAnexo(object @object, AnexoTipoEnum anexoTipo);
        Encoding IdentificarEncoding(string filename, out string text, int taster = 1000);       
        string LimparTextoDeCaracteresEspeciais(string texto);
        string LerArquivoEmbutido(string trechoNomeArquivo, Assembly assembly);       
        public string Base64Codificar(string plainText);
        public string Base64Decodificar(string base64EncodedData);
        public bool ConfirmarBase64(string base64String);
        Resultado<SearchIndexerClient> SelecionarSearchIndexerClient();
        MemoryStream GerarMemoryStreamString(string str);      
        string ComporURL(string action, string route, List<Dominio.Entidades.Parameter> parameters);
        #endregion

        #region Function / CS/ Open AI
        int CalcularNumeroTokens(string texto);
        Resultado<OpenAIResponse> FazerPerguntaChatGPT(string instrucoes, float Temperature, int MaxTokens);
        public Azure.Response<Azure.Storage.Queues.Models.SendReceipt> AdicionarItemQueue(string connection, string queueName, string mensagem, int maxRetries);
        string ComporQueryParaCognitiveSearch(string caminhoBlobStorage);
        Resultado<string> ComporPrompParaConsultaChatGPT(ref PerguntaResposta perguntaResposta);
        #endregion

        #region Blob
        Resultado<object> EnviarArquivoBlobStorage(string conn, string container, string caminhoCompletoArquivo, Stream stream, Dictionary<string, string> tags);
        Resultado<object> ExcluirArquivoBlobStorage(string conn, string blobContainerName, string caminhoArquivoBlobStorage);
        Dictionary<string, string> GerarTagsParaBlobPorAnexo(Anexo anexo);
        #endregion

    }
}
