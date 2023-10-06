namespace Dominio.Entidades
{
    public class UsuarioLogin
    {
        public Int64 Id { get; set; }
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual string UserId { get; set; }
        public virtual Usuario User { get; set; }
    }
}
