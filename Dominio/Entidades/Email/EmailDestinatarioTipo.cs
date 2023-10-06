namespace Dominio.Entidades
{
    public class EmailDestinatarioTipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<EmailDestinatario> EmailsDestinatarios { get; set; }
    }
}
