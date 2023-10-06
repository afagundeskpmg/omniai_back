using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class UsuarioListaViewModel
    {
        public UsuarioListaViewModel()
        {
            UsuarioViewModel = new UsuarioViewModel();
        }

        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public IEnumerable<Usuario> data { get; set; }      

        //[Display(Name = "Ambiente", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        public int? AmbienteSelecionadoId { get; set; }
        public IEnumerable<SelectListItem> AmbienteItens { get; set; }

        //[Display(Name = "Papel", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        public string PapelSelecionadoId { get; set; }
        public IEnumerable<SelectListItem> PapeisItens { get; set; }

        //[Display(Name = "Email", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        public string UserName { get; set; }

        //[Display(Name = "Nome", ResourceType = typeof(Resources.TextosLabelsPropriedades))]
        public string Nome { get; set; }
        public UsuarioViewModel UsuarioViewModel { get; set; }        
        public List<Papel> PapeisExistentes { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public string Email { get; set; }
        public bool Sucesso { get; set; }
        public bool? Excluido { get; set; }
        public int? ListaRetornoTipoId { get; set; }
        public string PapeisExcluidosIdsCsv { get; set; }
        public bool? ListarEmails { get; set; }
    }
}