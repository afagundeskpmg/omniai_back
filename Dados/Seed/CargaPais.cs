using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaPais
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new Pais() { Id = 1, Nome = "Brasil", Flag = "flag-icon-br", DocumentoPadrao = "CNPJ", CultureInfo = "pt-BR" },
                new Pais() { Id = 2, Nome = "Argentina", Flag = "flag-icon-ar", DocumentoPadrao = "", CultureInfo = "es-ES" },
                new Pais() { Id = 3, Nome = "Bolívia", Flag = "flag-icon-bo", DocumentoPadrao = "", CultureInfo = "es-ES" },
                new Pais() { Id = 4, Nome = "Colômbia", Flag = "flag-icon-co", DocumentoPadrao = "", CultureInfo = "es-ES" },
                new Pais() { Id = 5, Nome = "Guiana", Flag = "flag-icon-gf", DocumentoPadrao = "", CultureInfo = "en-US" },
                new Pais() { Id = 6, Nome = "Paraguai", Flag = "flag-icon-py", DocumentoPadrao = "", CultureInfo = "es-ES" },
                new Pais() { Id = 7, Nome = "Peru", Flag = "flag-icon-pe", DocumentoPadrao = "RUC", CultureInfo = "es-ES" },
                new Pais() { Id = 8, Nome = "Suriname", Flag = "flag-icon-sr", DocumentoPadrao = "", CultureInfo = "en-US" },
                new Pais() { Id = 9, Nome = "Uruguai", Flag = "flag-icon-uy", DocumentoPadrao = "", CultureInfo = "en-US" },
                new Pais() { Id = 10, Nome = "Venezuela", Flag = "flag-icon-ve", DocumentoPadrao = "", CultureInfo = "es-ES" },
                new Pais() { Id = 11, Nome = "Canada", Flag = "flag-icon-ca", DocumentoPadrao = "Registry ID", CultureInfo = "en-US" }
            };
        }
    }
}
