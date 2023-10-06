using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaPapelAcessivel
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
               //ADMIN KPMG
                new PapelAcessivel() { PapelId = "43e9fd1c-a4f2-4165-9cee-3edb4931f1af", PapelAcessanteId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                new PapelAcessivel() { PapelId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", PapelAcessanteId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                new PapelAcessivel() { PapelId = "8a55ed94-eece-4e7f-ab46-43185864b7d1", PapelAcessanteId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },

                //Cliente ADM
                 new PapelAcessivel() { PapelId = "8a55ed94-eece-4e7f-ab46-43185864b7d1", PapelAcessanteId = "8a55ed94-eece-4e7f-ab46-43185864b7d1"}
            };
        }
    }
}
