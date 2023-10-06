using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class EntidadeCadastroViewModel
    {
        [Required(ErrorMessage = "O parametro Nome é obrigatório")]
        [SwaggerSchema("Atribua um nome para a entidade")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O parametro Pergunta é obrigatório")]
        [SwaggerSchema("Defina uma pergunta para a entidade")]
        public string Pergunta { get; set; }
        [Required(ErrorMessage = "O parametro AmbientId é obrigatório")]
        [SwaggerSchema("O Id do tipo de documento.")]
        public string DocumentoTipoId { get; set; }
    }
}
