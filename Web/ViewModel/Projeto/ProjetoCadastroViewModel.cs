using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class ProjetoCadastroViewModel
    {
        
        [Required(ErrorMessage = "O parametro Nome é obrigatório.")]
        public string Nome { get; set; }                
        public int? AmbienteId { get; set; }
    }
}
