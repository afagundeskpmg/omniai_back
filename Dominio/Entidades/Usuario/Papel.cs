namespace Dominio.Entidades
{
    public class Papel
    {
        public Papel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<PapelClaim> Claims { get; set; }
        public virtual ICollection<PapelAcessivel> PapeisAcessiveis { get; set; }
        public virtual ICollection<PapelAcessivel> PapeisAcessantes { get; set; }

        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Nome,
                Alcadas = Claims.Count,
                Usuarios = Usuarios.Count
            };
        }
    }
}
