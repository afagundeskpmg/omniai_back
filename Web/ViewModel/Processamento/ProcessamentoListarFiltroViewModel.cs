using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class ProcessamentoListarFiltroViewModel
    {
        public string? Id { get; set; }
        public string? DataInicial { get; set; }
        public string? DataFinal { get; set; }     
        public string? Nome { get; set; }
        public int? AmbienteId { get; set; }
        public string? ProjetoId { get; set; }        
        public int? Start { get; set; }
        public int? Length { get; set; }
        public bool Excluido { get; set; }
        public int ProcessamentoTipoId { get; set; }
    }
}
