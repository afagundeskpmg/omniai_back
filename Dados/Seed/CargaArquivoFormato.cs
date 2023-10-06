using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaArquivoFormato
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new ArquivoFormato() { Id = 1, Nome = ".aac", MimeType = "audio/aac", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 2, Nome = ".abw", MimeType = "application/x-abiword", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 3, Nome = ".arc", MimeType = "application/octet-stream", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 4, Nome = ".avi", MimeType = "video/x-msvideo", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 5, Nome = ".azw", MimeType = "application/vnd.amazon.ebook", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 6, Nome = ".bin", MimeType = "application/octet-stream", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 7, Nome = ".bz", MimeType = "application/x-bzip", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 8, Nome = ".bz2", MimeType = "application/x-bzip2", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 9, Nome = ".csh", MimeType = "application/x-csh", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 10, Nome = ".css", MimeType = "text/css", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 11, Nome = ".csv", MimeType = "text/csv", TamanhoMaximoMb = 150 },
                new ArquivoFormato() { Id = 12, Nome = ".doc", MimeType = "application/msword", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 13, Nome = ".eot", MimeType = "application/vnd.ms-fontobject", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 14, Nome = ".epub", MimeType = "application/epub+zip", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 15, Nome = ".gif", MimeType = "image/gif", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 16, Nome = ".htm", MimeType = "text/html", TamanhoMaximoMb = 10 },
                new ArquivoFormato() { Id = 17, Nome = ".html", MimeType = "text/html", TamanhoMaximoMb = 10 },
                new ArquivoFormato() { Id = 18, Nome = ".ico", MimeType = "image/x-icon", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 19, Nome = ".ics", MimeType = "text/calendar", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 20, Nome = ".jar", MimeType = "application/java-archive", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 21, Nome = ".jpeg", MimeType = "image/jpeg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 22, Nome = ".jpg", MimeType = "image/jpeg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 23, Nome = ".js", MimeType = "application/javascript", TamanhoMaximoMb = 0 },
                new ArquivoFormato() { Id = 24, Nome = ".json", MimeType = "application/json", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 25, Nome = ".mid", MimeType = "audio/midi", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 26, Nome = ".midi", MimeType = "audio/midi", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 27, Nome = ".mpeg", MimeType = "video/mpeg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 28, Nome = ".mpkg", MimeType = "application/vnd.apple.installer+xml", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 29, Nome = ".odp", MimeType = "application/vnd.oasis.opendocument.presentation", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 30, Nome = ".ods", MimeType = "application/vnd.oasis.opendocument.spreadsheet", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 31, Nome = ".odt", MimeType = "application/vnd.oasis.opendocument.text", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 32, Nome = ".oga", MimeType = "audio/ogg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 33, Nome = ".ogv", MimeType = "video/ogg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 34, Nome = ".ogx", MimeType = "application/ogg", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 35, Nome = ".otf", MimeType = "font/otf", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 36, Nome = ".png", MimeType = "image/png", TamanhoMaximoMb = 10 },
                new ArquivoFormato() { Id = 37, Nome = ".pdf", MimeType = "application/pdf", TamanhoMaximoMb = 100 },
                new ArquivoFormato() { Id = 38, Nome = ".ppt", MimeType = "application/vnd.ms-powerpoint", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 39, Nome = ".rar", MimeType = "application/x-rar-compressed", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 40, Nome = ".rtf", MimeType = "application/rtf", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 41, Nome = ".sh", MimeType = "application/x-sh", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 42, Nome = ".svg", MimeType = "image/svg+xml", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 43, Nome = ".swf", MimeType = "application/x-shockwave-flash", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 44, Nome = ".tar", MimeType = "application/x-tar", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 45, Nome = ".tif", MimeType = "image/tiff", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 46, Nome = ".tiff", MimeType = "image/tiff", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 47, Nome = ".ts", MimeType = "application/typescript", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 48, Nome = ".ttf", MimeType = "font/ttf", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 49, Nome = ".vsd", MimeType = "application/vnd.visio", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 50, Nome = ".wav", MimeType = "audio/x-wav", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 51, Nome = ".weba", MimeType = "audio/webm", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 52, Nome = ".webm", MimeType = "video/webm", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 53, Nome = ".webp", MimeType = "image/webp", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 54, Nome = ".woff", MimeType = "font/woff", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 55, Nome = ".woff2", MimeType = "font/woff2", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 56, Nome = ".xhtml", MimeType = "application/xhtml+xml", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 57, Nome = ".xls", MimeType = "application/vnd.ms-excel", TamanhoMaximoMb = 150 },
                new ArquivoFormato() { Id = 58, Nome = ".xlsx", MimeType = "application/vnd.ms-excel", TamanhoMaximoMb = 150 },
                new ArquivoFormato() { Id = 59, Nome = ".xml", MimeType = "application/xml", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 60, Nome = ".xul", MimeType = "application/vnd.mozilla.xul+xml", TamanhoMaximoMb = 5 },
                new ArquivoFormato() { Id = 61, Nome = ".zip", MimeType = "application/zip", TamanhoMaximoMb = 80 },
                new ArquivoFormato() { Id = 62, Nome = ".txt", MimeType = "application/txt", TamanhoMaximoMb = 10 },
                new ArquivoFormato() { Id = 63, Nome = ".docx", MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", TamanhoMaximoMb = 5 }
            };
        }
    }
}
