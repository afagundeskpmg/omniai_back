using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaPapel
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new Papel() { Id = "43e9fd1c-a4f2-4165-9cee-3edb4931f1af", Nome = "Sistema" },
                new Papel() { Id = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", Nome = "KPMG" },
                new Papel() { Id = "8a55ed94-eece-4e7f-ab46-43185864b7d1", Nome = "Administrador" }
            };
        }
    }
}
