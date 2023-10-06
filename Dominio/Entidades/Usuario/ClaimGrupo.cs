namespace Dominio.Entidades
{
    public class ClaimGrupo
    {
        public ClaimGrupo()
        {
            ClaimsImpactantes = new List<RelacaoClaimGrupo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public virtual List<RelacaoClaimGrupo> ClaimsImpactantes { get; set; }
    }
}
