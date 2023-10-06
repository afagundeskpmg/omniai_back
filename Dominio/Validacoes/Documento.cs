using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio.Validacoes
{
    public static class Documento
    {
        public static bool Valido(string doc)
        {
            string documento = string.Join("", Regex.Split(doc, @"[^\d]"));

            if (documento == null)
                return false;

            if (documento.Count() == 14)
                return CNPJ.Valido(documento);
            else if (documento.Count() == 11)
            {
                return (CPF.Valido(documento) || RUC.Valido(documento));
            }
            else return false;
        }
        public static string Formatar(string doc)
        {
            string documento = string.Empty;

            if (!string.IsNullOrEmpty(doc))
            {
                documento = string.Join("", Regex.Split(doc, @"[^\d]"));

                if (documento.Count() == 14)
                    CNPJ.Formatar(ref documento);
                else if (documento.Count() == 11 && CPF.Valido(documento))
                    CPF.Formatar(ref documento);
                else if (documento.Count() == 6)
                    CPF.FormatarLGPD(ref documento);
            }

            return documento;
        }

        public static string RemoverCaracterEspecialDocumento(string documento)
        {
            return string.Join("", Regex.Split(documento, @"[^\d]"));
        }
    }
}
