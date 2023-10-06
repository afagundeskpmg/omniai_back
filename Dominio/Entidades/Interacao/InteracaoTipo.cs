namespace Dominio.Entidades
{
    public class InteracaoTipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Interacao> Interacoes { get; set; }
    }
}
