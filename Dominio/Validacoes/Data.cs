using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Validacoes
{
    public static class Data
    {
        public static bool Valido(string data)
        {
            DateTime resultado;

            if (string.IsNullOrEmpty(data))
                return false;
            else if (DateTime.TryParse(data, out resultado))
                return true;
            else
                return false;
        }
    }
}
