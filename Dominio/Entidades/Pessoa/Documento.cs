namespace Dominio.Entidades
{
    public class Documento
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Mascara { get; set; }
        public virtual bool Principal { get; set; }
        public virtual int? PessoaTipoId { get; set; }
        public virtual PessoaTipo PessoaTipo { get; set; }
        public virtual int? PaisId { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
