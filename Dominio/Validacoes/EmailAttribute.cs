using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Validacoes
{
    public class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = value == null ? "" : value;
            if (Email.Valido((String)value))
                return null;
            else
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
