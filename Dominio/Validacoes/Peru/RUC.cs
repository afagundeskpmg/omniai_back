using System;

namespace Dominio.Validacoes
{
    public static class RUC
    {
        public static bool Valido(string ruc)
        {
            if (ruc.Length != 11)
                return false;

            int dig01 = Convert.ToInt32(ruc.Substring(0, 1)) * 5;
            int dig02 = Convert.ToInt32(ruc.Substring(1, 1)) * 4;
            int dig03 = Convert.ToInt32(ruc.Substring(2, 1)) * 3;
            int dig04 = Convert.ToInt32(ruc.Substring(3, 1)) * 2;
            int dig05 = Convert.ToInt32(ruc.Substring(4, 1)) * 7;
            int dig06 = Convert.ToInt32(ruc.Substring(5, 1)) * 6;
            int dig07 = Convert.ToInt32(ruc.Substring(6, 1)) * 5;
            int dig08 = Convert.ToInt32(ruc.Substring(7, 1)) * 4;
            int dig09 = Convert.ToInt32(ruc.Substring(8, 1)) * 3;
            int dig10 = Convert.ToInt32(ruc.Substring(9, 1)) * 2;
            int dig11 = Convert.ToInt32(ruc.Substring(10, 1));

            int soma = dig01 + dig02 + dig03 + dig04 + dig05 + dig06 + dig07 + dig08 + dig09 + dig10;
            int resto = soma % 11;
            int diferenca = 11 - resto;

            int digChk = 0;
            if (diferenca == 10)
                digChk = 0;
            else if (diferenca == 11)
                digChk = 1;
            else
                digChk = diferenca;

            if (dig11 == digChk)
                return true;
            else
                return false;
        }
        public static void Formatar(ref string CNPJ)
        {
           CNPJ = Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
