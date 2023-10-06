namespace Dominio.Entidades
{
    public class Pais
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Flag { get; set; }
        public string DocumentoPadrao { get; set; }
        public string CultureInfo { get; set; }       
        public virtual ICollection<Pessoa> Pessoas { get; set; }     
        public virtual ICollection<Documento> DocumentoTipos { get; set; }      
    }
}
