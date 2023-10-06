using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaAnexoTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new AnexoTipo() { Id = (int)AnexoTipoEnum.EmailCorpo, Nome = "Email Corpo" },
                new AnexoTipo() { Id = (int)AnexoTipoEnum.Documento, Nome = "Documento" },
                new AnexoTipo() { Id = (int)AnexoTipoEnum.DocumentoPagina, Nome = "Documento Página" }
            };
        }
    }
}
