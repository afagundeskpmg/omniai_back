using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Validacoes
{
    public static class BusinessNumber
    {
        public static bool Valido(string number)
        {
            if (number.Length < 3 || number.Length > 10)
                return false;
            else
                return true;
        }

        //COMENTADO DEVIDO AO REGISTRY ID NÃO SER VALIDADO
        //public static bool Valido(string bn)
        //{
        //    bn = bn.RemoveSpecialCharacthers();

        //    var chardDigits = bn.ToCharArray();
        //    if (chardDigits.Length != 9)
        //    {
        //        return false;
        //    }

        //    int[] digits = new int[chardDigits.Length];
        //    for (int i = 0; i < chardDigits.Length; i++)
        //    {
        //        if (!int.TryParse(chardDigits[i].ToString(), out digits[i]))
        //        {
        //            return false;
        //        }
        //    }

        //    var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum()
        //                + digits.Where((value, index) => index % 2 != 0).Select(v => v * 2)
        //                      .SelectMany(v => v.ToDigitEnumerable()).Sum();

        //    var checkDigit = (10 - (total % 10)) % 10;

        //    bool isValid = digits.Last() == checkDigit;
        //    return isValid;
        //}
        private static string RemoveSpecialCharacthers(this string ssn)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ssn?.Length; i++)
            {
                if (char.IsLetterOrDigit(ssn[i]))
                {
                    sb.Append(ssn[i]);
                }
            }
            return sb.ToString();
        }

        private static IEnumerable<int> ToDigitEnumerable(this int number)
        {
            IList<int> digits = new List<int>();

            while (number > 0)
            {
                digits.Add(number % 10);
                number = number / 10;
            }

            return digits.Reverse();
        }
    }
}
