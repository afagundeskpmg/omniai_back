using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dominio.Validacoes
{
    public class Nome
    {
        public static string RemoverAcentos(string nome)
        {
            var retorno = new StringBuilder();
            var arrayText = nome.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    retorno.Append(letter);

            return retorno.ToString();
        }

        public static string NormalizarNome(string nome)
        {
            return Regex.Replace(RemoverAcentos(nome), @"\s+", " ").Trim().ToUpper();
        }

        public static string Capitalizar(string texto)
        {
            return (string.IsNullOrEmpty(texto) ? "" : texto.First().ToString().ToUpper() + texto.Substring(1));
        }

        public static string RemoverCaracteresEspeciais(string palavra)
        {
            return string.Join("", Regex.Split(palavra, "[^0-9a-zA-Z]+"));
        }

        public static bool CompararNomes(string nome1, string nome2)
        {
            // Remove o acento do segundo nome
            string nomeTemp1 = RemoverAcentos(nome1).ToUpper().Trim();
            string nomeTemp2 = RemoverAcentos(nome2).ToUpper().Trim();

            var nomes1 = nomeTemp1.Split(' ');
            var nomes2 = nomeTemp2.Split(' ');

            // Se tiver um nome e for igual dá match
            return (nomes1.Length == 1 && nomes2.Length == 1 && nomeTemp1 == nomeTemp2)
                // Se não, compara os últimos, apenas se os dois tiverem mais de 1
                // para que "fulano" seja diferente de "fulano fulano" ou "fulano ciclano fulano" e assim por diante
                || (nomes1.Length > 1 && nomes2.Length > 1 && nomes1[0] == nomes2[0] && nomes1.Last() == nomes2.Last());
        }
    }
}
