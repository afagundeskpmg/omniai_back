namespace Dominio.Entidades
{
    public class Log
    {
        public string ServidorNome { get; set; }
        public string URL { get; set; }
        public string UserAgent { get; set; }
        public string IP { get; set; }
        public Usuario Usuario { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Mensagem { get; set; }

        public string Imprimir()
        {
            var log = new System.Text.StringBuilder();

            var descricao = "[" + CriadoEm.ToString("dd/MM/yyyy HH:mm:ss") + "]";
            if (!string.IsNullOrEmpty(ErrorMessage))
                descricao += " - " + ErrorMessage;

            log.AppendLine(descricao);
            if (!string.IsNullOrEmpty(InnerException))
                log.AppendLine("    " + InnerException);

            if (!string.IsNullOrEmpty(StackTrace) && !log.ToString().Contains(StackTrace))
                log.AppendLine(StackTrace);

            if (!string.IsNullOrEmpty(ServidorNome))
                log.AppendLine("    " + ServidorNome);

            if (!string.IsNullOrEmpty(URL))
                log.AppendLine("    " + URL);

            if (!string.IsNullOrEmpty(UserAgent))
                log.AppendLine("    " + UserAgent);

            if (!string.IsNullOrEmpty(IP))
                log.AppendLine("    " + IP);

            if (Usuario != null)
                log.AppendLine("    Usuário: " + Usuario.UserName);

            if (!string.IsNullOrEmpty(Mensagem))
                log.AppendLine("    " + Mensagem);

            return log.ToString();
        }
    }
}

