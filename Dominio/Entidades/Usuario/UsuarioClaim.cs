namespace Dominio.Entidades
{
    public class UsuarioClaim
    {
        public UsuarioClaim() { }
        public UsuarioClaim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        
        public virtual string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
    }
}
