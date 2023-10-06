namespace Dominio.Entidades
{
    public class PapelClaim
    {
        public string PapelId { get; set; }
        public virtual Papel Papel { get; set; }
        public int ClaimId { get; set; }
        public virtual Claim Claim { get; set; }
    }
}
