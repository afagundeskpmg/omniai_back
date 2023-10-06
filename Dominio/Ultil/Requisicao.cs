using System.Text;

namespace Dominio.Entidades
{
    public class Requisicao
    {
        public Requisicao()
        {
            Parameters = new List<Parameter>();
            Headers = new List<Header>();
            CaminhosArquivos = new List<CaminhoArquivo>();
        }

        public Requisicao(string url, string method, string mediaType, int? timeOut)
        {
            Parameters = new List<Parameter>();
            Headers = new List<Header>();
            CaminhosArquivos = new List<CaminhoArquivo>();
            Uri = url;
            Method = method;
            MediaType = mediaType;
            TimeOutMilisegundos = timeOut;
        }

        public Requisicao(string url, string method, string mediaType, string token, string autorizacao, int? timeOut, string postData)
        {
            Parameters = new List<Parameter>();
            Headers = new List<Header>();
            CaminhosArquivos = new List<CaminhoArquivo>();
            Uri = url;
            Method = method;
            MediaType = mediaType;
            TimeOutMilisegundos = timeOut;
            Token = token;
            Autorizacao = autorizacao;
            PostData = postData;
        }

        public Requisicao(string url, string method, string mediaType, string token, string autorizacao, int? timeOut, string postData, string contentType)
        {
            Parameters = new List<Parameter>();
            Headers = new List<Header>();
            CaminhosArquivos = new List<CaminhoArquivo>();
            Uri = url;
            Method = method;
            MediaType = mediaType;
            TimeOutMilisegundos = timeOut;
            Token = token;
            Autorizacao = autorizacao;
            PostData = postData;
            ContentType = contentType;
        }

        public string Uri { get; set; }
        public string ContentType { get; set; }
        public string MediaType { get; set; }
        public string Method { get; set; }
        public string PostData { get; set; }
        public string Token { get; set; }
        public string Autorizacao { get; set; }
        public int? TimeOutMilisegundos { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public string Host { get; set; }
        public string Cookies { get; set; }
        public Encoding EncodingRetorno { get; set; }
        public Encoding EncodingEnvio { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Parameter> Parameters { get; set; }
        public List<CaminhoArquivo> CaminhosArquivos { get; set; }
        public string Connection { get; set; }
        public string Referer { get; set; }
    }

    public class Header
    {
        public Header(string nome, string valor) { Nome = nome; Valor = valor; }
        public string Nome { get; set; }
        public string Valor { get; set; }
    }

    public class Parameter
    {
        public Parameter(string nome, string valor) { Nome = nome; Valor = valor; }
        public string Nome { get; set; }
        public string Valor { get; set; }
    }

    public class CaminhoArquivo
    {
        public CaminhoArquivo(string parametro, string caminho, string contentType) { Parametro = parametro; Caminho = caminho; ContentType = contentType; }
        public string Parametro { get; set; }
        public string Caminho { get; set; }
        public string ContentType { get; set; }
    }
}
