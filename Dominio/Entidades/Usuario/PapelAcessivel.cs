namespace Dominio.Entidades
{
    public class PapelAcessivel
    {
        public string PapelId { get; set; }
        public virtual Papel Papel { get; set; }
        public string PapelAcessanteId { get; set; }
        public virtual Papel PapelAcessante { get; set; }
    }
}
