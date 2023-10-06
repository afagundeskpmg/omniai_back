using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaPessoaTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new PessoaTipo() { Id = 1, Nome = "Fisica" },
                new PessoaTipo() { Id = 2, Nome = "Juridica" }
            };
        }
    }
}
