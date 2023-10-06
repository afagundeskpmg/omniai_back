using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaUsuarioClaim
    {
        public static List<object> GerarCarga()
        {
            var claims = CargaClaim.GerarCarga().Cast<Claim>();
            var usuarioClaim = new List<object>();

            foreach (var claim in claims)
            {
                usuarioClaim.Add(new UsuarioClaim() { UsuarioId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", ClaimType = claim.ClaimType, ClaimValue = claim.DefaultValue });
                usuarioClaim.Add(new UsuarioClaim() { UsuarioId = "f60753d7-c5a7-4496-a360-c1a301d87763", ClaimType = claim.ClaimType, ClaimValue = claim.DefaultValue });
            }

            return usuarioClaim;
        }
    }
}