using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class APIGetTokenViewModel
    {
        [Required(ErrorMessage = "O campo username é requerido")]        
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
        [MaxLength(100, ErrorMessage = "O campo username deve ter no maximo 100 caracteres")]
        public string username { get; set; }

        [Required(ErrorMessage = "O campo password é requerido")]
        [DataType(DataType.Password)]        
        [StringLength(50, ErrorMessage = "O campo password deve ter entre 6 e 50 caracteres", MinimumLength = 6)]
        public string password { get; set; }
    }
}