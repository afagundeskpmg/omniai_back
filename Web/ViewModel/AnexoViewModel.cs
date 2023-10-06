using Dominio.Entidades;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.ViewModel{
    
    public class AnexoViewModel 
    {
        [SwaggerSchema("O arquivo a ser enviado.")]
        public IFormFile FileUpload { get; set; }

        [SwaggerSchema("O ID do tipo de anexo.")]
        public int AnexoTipoId { get; set; }

        [SwaggerSchema("O ID do tipo de arquivo de anexo.")]
        public int AnexoArquivoTipoId { get; set; }

        [SwaggerSchema("O ID do objeto relacionado.")]
        public string ObjetoId { get; set; }

        [SwaggerSchema("Parâmetros adicionais em formato chave-valor.")]
        public Dictionary<string, object> ParametrosAdicionais { get; set; }   
    }
}