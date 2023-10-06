using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacoes
{
    public class RUCAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = value == null ? "" : value;
            if (RUC.Valido((String)value))
                return null;
            else
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
