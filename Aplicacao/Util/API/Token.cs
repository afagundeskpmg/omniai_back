using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Util.API.B2C
{
    public class Token
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string access_token { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public object SerializarResultado()
        {
            if (!string.IsNullOrEmpty(error))
            {
                return new
                {
                    error,
                    error_description
                };
            }
            else
            {
                return new
                {
                    token_type,
                    expires_in,
                    ext_expires_in,
                    access_token
                };
            }
        }
    }
}
