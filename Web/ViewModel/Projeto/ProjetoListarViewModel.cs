using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class ProjetoListarViewModel
    {
      
        public string? DataInicial { get; set; }
        public string? DataFinal { get; set; }     
        public string? Nome { get; set; }
        public int? AmbienteId { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
        public bool Excluido { get; set; }
    }
}
