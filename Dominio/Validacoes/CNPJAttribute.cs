using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacoes
{
    public class CNPJAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = value == null ? "" : value;
            if (CNPJ.Valido((String)value))
                return null;
            else
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
