using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class DocumentoTipoCadastroViewModel
    {
        [Required(ErrorMessage = "O parametro Nome é obrigatório")]
        [SwaggerSchema("Atribua um nome para o tipo de documento")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O parametro AmbientId é obrigatório")]
        [SwaggerSchema("O Id do ambiente.")]
        public int? AmbienteId { get; set; }
    }
    
}
