using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio.Validacoes
{
    public static class CPF
    {
        public static bool Valido(string cpf)
        {
            if (cpf == null)
                return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static void Formatar(ref string CPF)
        {
            CPF = Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        public static void FormatarLGPD(ref string CPF)
        {
            CPF = "***." + Convert.ToUInt64(CPF).ToString(@"000\.000") + "-**";
        }

        public static string NormalizarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return null;
            else
            {
                var cpfLimpo = Regex.Replace(cpf, @"[^\d]", "");

                if (cpfLimpo.Count() == 11)
                    return cpfLimpo;
                else
                    return Convert.ToUInt64(cpfLimpo.Replace("000***", "")).ToString(@"***000000\**");
            }
        }
    }
}
