namespace Dominio.Entidades
{
    public class RelacaoClaimGrupo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ClaimId { get; set; }
        public virtual Claim Claim { get; set; }
        public int ClaimGrupoId { get; set; }
        public virtual ClaimGrupo Grupo { get; set; }
    }
}
