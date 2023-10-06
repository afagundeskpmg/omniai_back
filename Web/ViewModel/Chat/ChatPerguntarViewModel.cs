using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel.Projeto
{
    public class ChatPerguntarViewModel
    {
        [Required(ErrorMessage = "O parametro Id é obrigatório")]
        public string ProjetoId { get; set; }
        [Required(ErrorMessage = "O parametro Pergunta é obrigatório")]
        public string Pergunta { get; set; }
    }
}
