namespace Dominio.Entidades
{
    public abstract class EmailDestinatario
    {
        public int Id { get; set; }
        public int EmailDestinatarioTipoId { get; set; }
        public virtual EmailDestinatarioTipo EmailDestinatarioTipo { get; set; }
        public int EmailId { get; set; }
        public virtual Email Email { get; set; }
    }
}
