namespace Dominio.Entidades
{
    public class PessoaTipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }      
        public virtual ICollection<Documento> DocumentoTipos { get; set; }
    }
}
