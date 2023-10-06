using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class Claim
    {
        public Claim()
        {
            GruposImpactados = new List<RelacaoClaimGrupo>();
            ClaimsFilhas = new List<Claim>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int? ClaimPaiId { get; set; }
        public virtual Claim ClaimPai { get; set; }
        public string ClaimType { get; set; }
        public string DefaultValue { get; set; }
        public string Descricao { get; set; } 
        public virtual List<Claim> ClaimsFilhas { get; set; }
        public virtual ICollection<PapelClaim> Papeis { get; set; }
        public virtual List<RelacaoClaimGrupo> GruposImpactados { get; set; }

        public List<string> ClaimsDependentes
        {
            get
            {
                var retorno = new List<string>();

                if (ClaimPai != null)
                {
                    retorno.Add(ClaimPai.Descricao);
                    retorno.AddRange(ClaimPai.ClaimsDependentes);
                }

                return retorno;
            }
        }

        public List<int> IdsPai
        {
            get
            {
                var retorno = new List<int>();

                if (ClaimPai != null)
                {
                    retorno.Add((int)ClaimPaiId);
                    retorno.AddRange(ClaimPai.IdsPai);
                }

                return retorno;
            }
        }

        public List<int> IdsFilhas
        {
            get
            {
                var retorno = new List<int>();
                if (ClaimsFilhas != null && ClaimsFilhas.Any())
                {
                    foreach (var claim in ClaimsFilhas)
                    {
                        retorno.Add(claim.Id);
                        retorno.AddRange(claim.IdsFilhas);
                    }
                }

                return retorno;
            }
        }
    }
}
