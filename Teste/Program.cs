
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using SixLabors.Fonts.Tables.AdvancedTypographic;
using System.ComponentModel;
using Teste;
using Aplicacao.Aplicacao;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Configuration;
using Dominio.Entidades;
using Azure.Search.Documents.Indexes.Models;
using Aplicacao.Util.OpenAI;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Azure;
using Microsoft.Identity.Client;
using System.Drawing;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
        string question = "Qual classificação do documento?";



        using (UnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
        {
            // _aplicacao.ConfigurarEntidadesNoUnitOfWork("PerguntaResposta");

            var entidade = _aplicacao.Entidade.SelecionarFirstOrDefault();
            var processamentoIndexer = _aplicacao.ProcessamentoIndexer.SelecionarFirstOrDefault(x=> x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComSucesso);
            var projetoAnexo = processamentoIndexer.Anexos.FirstOrDefault();
            var caminhoConsulta = string.Concat(projetoAnexo.Anexo.BlobContainerName, "/", processamentoIndexer.BlobFolder, projetoAnexo.Anexo.Id);
            var parametrosConsulta = string.Concat("https://omniaistorage.blob.core.windows.net/", caminhoConsulta);
            var tokens = _aplicacao.Processamento.SelecionarParametro(ParametroAmbienteEnum.AzureOpenAIMaxTokenQueryQuestion);
            var temperature = _aplicacao.Processamento.SelecionarParametro(ParametroAmbienteEnum.AzureOpenAITemperatureQueryQuestion);
            var prompt = "Responda à pergunta com a maior sinceridade possível usando o texto fornecido e, se a resposta não estiver contida no texto abaixo, diga 'informação não encontrada' \n\n Context:\n {0}\n\n Q:{1} \n A:";
            var query = _aplicacao.Processamento.SelecionarParametro(ParametroAmbienteEnum.AzureCognitiveSearchQuery);

            SelecionarConteudoCognitiveSearch("search.ismatch('\"https://omniaistorage.blob.core.windows.net/anexos/Ambiente/1/Anexos/Projetos/237c3fbb-ef95-473d-9d3b-564a97ab88cf\"','metadata_storage_path')",3);
            string content = Select(query: entidade.Query, filter_string: "search.ismatch('\"https://omniaistorage.blob.core.windows.net/anexos/Ambiente/1/Anexos/Projetos/237c3fbb-ef95-473d-9d3b-564a97ab88cf\"','metadata_storage_path')");
            prompt = string.Format(prompt, content, question);


            var resultado = _aplicacao.Processamento.FazerPerguntaChatGPT(prompt, float.Parse("0,5"), 1024);
        }
    }
    public static string SelecionarConteudoCognitiveSearch(string query, int size)
    {
        var resultado = new Resultado<List<IndexContent>>() { Retorno = new List<IndexContent>() };

        var resultadoSearchClient = SelecionarSearchClient("teste-index");
        if (!resultadoSearchClient.Sucesso)
            resultado.AtribuirMensagemErro(resultadoSearchClient);
        else
        {
            var select = new List<string> { "id", "content", "metadata_storage_name", "metadata_storage_path" };
            SearchOptions options = new SearchOptions() { Size = size, Filter = query, IncludeTotalCount = true };
            select.ForEach(s => options.Select.Add(s));

            SearchResults<DocumentModel> results = resultadoSearchClient.Retorno.Search<DocumentModel>("", options);

            if (results.TotalCount == null || results.TotalCount == 0)
                resultado.Mensagem = "Desculpe, mas não encontramos resultados para a sua consulta na index. Por favor, verifique os critérios da sua pesquisa e tente novamente.";
            else
            {
                foreach (SearchResult<DocumentModel> result in results.GetResults())
                {
                    resultado.Retorno.Add(new IndexContent() { FileName = result.Document.metadata_storage_name, Content = result.Document.content });
                }
                resultado.Sucesso = true;
            }
        }

        return "";
    }
    private static Resultado<SearchClient> SelecionarSearchClient(string indexName)
    {
        var resultado = new Resultado<SearchClient>();
        try
        {
            string apiKey = "qgou7Z7W5dqCGCchqCVG78JmXzegYITzjmndbQUhWFAzSeDbplu6";

            // Create a SearchIndexClient to send create/delete index commands
            Uri serviceEndpoint = new Uri("https://omniaisearch.search.windows.net");
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);
            SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

            // Create a SearchClient to load and query documents
            resultado.Retorno = new SearchClient(serviceEndpoint, indexName, credential);
            resultado.Sucesso = true;
        }
        catch (Exception ex)
        {
            resultado.AtribuirMensagemErro(ex, "Ocorreu um erro ao tentar conectar-se ao cognitive search");
        }

        return resultado;
    }
    public static string Select(string query, string filter_string)
    {
        var resultado = new Resultado<object>();
        string concat_content = "";

        SearchClient searchClient = Get_search_client("teste-index");
        SearchOptions options = new SearchOptions() { Size = 3, Filter = filter_string, IncludeTotalCount = true, };
        SearchResults<DocumentModel> results;

        //Parametrizar output
        options.Select.Add("id");
        options.Select.Add("content");
        options.Select.Add("metadata_storage_name");
        options.Select.Add("metadata_storage_path");

        results = searchClient.Search<DocumentModel>("", options);

        if (results.TotalCount == null)

            foreach (SearchResult<DocumentModel> result in results.GetResults())
            {
                concat_content += string.Concat(result.Document.metadata_storage_name, result.Document.content);
            }


        return concat_content;

    }

    private static SearchClient Get_search_client(string index_name)
    {

        string serviceName = "omniaisearch";
        string apiKey = "qgou7Z7W5dqCGCchqCVG78JmXzegYITzjmndbQUhWFAzSeDbplu6";
        string indexName = index_name;

        // Create a SearchIndexClient to send create/delete index commands
        Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
        AzureKeyCredential credential = new AzureKeyCredential(apiKey);
        SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

        // Create a SearchClient to load and query documents
        SearchClient searchClient = new SearchClient(serviceEndpoint, indexName, credential);

        return searchClient;
    }

    private static SearchIndexerClient Get_indexer_client()
    {
        Console.WriteLine("Creating Indexer Client...");

        return new SearchIndexerClient(new Uri("https://omniaisearch.search.windows.net"), new AzureKeyCredential("ByLiZz2EjMvuSKRdAyEocGXfBTERKzk2DmsKKcElEJAzSeCEeKRN"));

    }

}

