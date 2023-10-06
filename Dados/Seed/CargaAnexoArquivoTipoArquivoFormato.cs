using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaAnexoArquivoTipoArquivoFormato
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.CSV},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.DOC},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.DOCX},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.GIF},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.JPG},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.JPEG},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.PNG},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.PDF},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.PPT},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.XLS},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.XLSX},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Outros, ArquivoFormatoId = (int)ArquivoFormatoEnum.ZIP},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Carga, ArquivoFormatoId = (int)ArquivoFormatoEnum.CSV},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Carga, ArquivoFormatoId = (int)ArquivoFormatoEnum.XLS},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Carga, ArquivoFormatoId = (int)ArquivoFormatoEnum.XLSX},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Carga, ArquivoFormatoId = (int)ArquivoFormatoEnum.ZIP},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.PaginaHTML, ArquivoFormatoId = (int)ArquivoFormatoEnum.HTM},
                new AnexoArquivoTipoArquivoFormato() { AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.Documento, ArquivoFormatoId = (int)ArquivoFormatoEnum.PDF},
            };
        }
    }
}
