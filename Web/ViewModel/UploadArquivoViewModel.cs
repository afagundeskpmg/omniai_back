using Microsoft.ApplicationInsights.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class UploadArquivoViewModel
    {
        [SwaggerSchema("Este campo refere-se ao arquivo a ser enviado.")]
        [Required(ErrorMessage = "O preenchimento do campo 'Arquivo' é obrigatório.")]
        public IFormFile Arquivo { get; set; }

        [SwaggerSchema("Identificação do projeto relacionado.")]
        [Required(ErrorMessage = "O preenchimento do campo 'ProjetoId' é obrigatório.")]
        public string ProjetoId { get; set; }

        [SwaggerSchema("ID do tipo de documento relacionado.")]
        public string? DocumentoTipoId { get; set; }
    }
}
