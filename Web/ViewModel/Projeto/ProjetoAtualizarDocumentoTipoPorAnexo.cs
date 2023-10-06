using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel.Projeto
{
    public class ProjetoAtualizarDocumentoTipoPorAnexo
    {
        [SwaggerSchema("Este campo refere-se ao identificador do projeto.")]
        [Required(ErrorMessage = "O preenchimento do campo 'ProjetoId' é obrigatório.")]
        public string ProjetoId { get; set; }
        [SwaggerSchema("Este campo refere-se ao identificador do anexo/arquivo.")]
        [Required(ErrorMessage = "O preenchimento do campo 'AnexoId' é obrigatório.")]
        public int AnexoId { get; set; }
        [SwaggerSchema("Este campo refere-se ao identificador do documento.")]
        [Required(ErrorMessage = "O preenchimento do campo 'DocumentoTipoId' é obrigatório.")]
        public string DocumentoTipoId { get; set; }

    }
}
