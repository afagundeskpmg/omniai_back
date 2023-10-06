using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
        }
        [EmailAddress]
        //[Display(Name = "Email", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        //[Required(ErrorMessageResourceName = "CampoObrigatorio", ErrorMessageResourceType = typeof(Resources.TextosValidacoes))]
        public string Email { get; set; }
        //[Display(Name = "Nome", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        public string Nome { get; set; }

        //[Display(Name = "PapelSelecionado", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        //[Required(ErrorMessageResourceName = "CampoObrigatorio", ErrorMessageResourceType = typeof(Resources.TextosValidacoes))]
        public string PapelSelecionadoId { get; set; }
        public IEnumerable<SelectListItem>? PapeisItens { get; set; }

        //[Display(Name = "Ambiente", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        //[Required(ErrorMessageResourceName = "CampoObrigatorio", ErrorMessageResourceType = typeof(Resources.TextosValidacoes))]
        public int? AmbienteSelecionadoId { get; set; }
        public IEnumerable<SelectListItem>? AmbienteItens { get; set; }
    }
}