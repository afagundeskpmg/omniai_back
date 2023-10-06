using Newtonsoft.Json;

namespace Dominio.Entidades
{
    public class Resultado<T>
    {
        public bool Sucesso { get; set; }        
        public string Mensagem { get; set; }
        [JsonIgnore]
        public string MensagemInterna { get; set; }
        public T Retorno { get; set; }
        public void AtribuirMensagemErro<T>(Resultado<T> resultado)
        {
            Mensagem = resultado.Mensagem; 
            MensagemInterna=resultado.MensagemInterna; 
        }
        public void AtribuirMensagemErro(Exception ex, string mensagem)
        {
            Mensagem = mensagem;
            MensagemInterna = ex.InnerException !=null ? ex.InnerException.Message : ex.Message;
        }
    }
    

}
