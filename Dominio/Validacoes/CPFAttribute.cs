using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacoes
{
    public class CPFAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = value == null ? "" : value;
            if (CPF.Valido((String)value))
                return null;
            else
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
