namespace Dominio.Entidades
{
    public class EmailConfig
    {
        public EmailConfig()
        {
            Anexos = new List<Anexo>();
        }

        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public List<Anexo> Anexos { get; set; }
    }
}
