namespace Dominio.Entidades
{
    public class Email
    {
        public Email() { }
        public Email(string usuarioId)
        {
            ProcessamentoEmail = new ProcessamentoEmail(usuarioId, this);
        }

        public int Id { get; set; }
        public string Assunto { get; set; }
        public int CorpoAnexoId { get; set; }
        public virtual Anexo CorpoAnexo { get; set; }
        public string RemetenteEmail { get; set; }        
        public virtual ICollection<EmailDestinatario> EmailsDestinatarios { get; set; }
        public DateTime? EnviadoEm { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual ProcessamentoEmail ProcessamentoEmail { get; set; }
    }
}
