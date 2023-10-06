namespace Dominio.Entidades
{
    public class AnexoTipo
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }
    }
}
