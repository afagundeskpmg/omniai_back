using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaInteracaoTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new InteracaoTipo() { Id = 1, Nome = "Ambiente" },
                new InteracaoTipo() { Id = 2, Nome = "Usuario" },
                new InteracaoTipo() { Id = 3, Nome = "Projeto" },
            };
        }
    }
}
