using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaAnexoArquivoTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new AnexoArquivoTipo() { Id = (int)AnexoArquivoTipoEnum.Outros, Nome = "Outros" },
                new AnexoArquivoTipo() { Id = (int)AnexoArquivoTipoEnum.Carga, Nome = "Carga" },
                new AnexoArquivoTipo() { Id = (int)AnexoArquivoTipoEnum.PaginaHTML, Nome = "Página HTML" },
                new AnexoArquivoTipo() { Id = (int)AnexoArquivoTipoEnum.Documento, Nome = "Documento" },
            };
        }
    }
}
