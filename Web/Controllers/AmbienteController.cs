using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    [Authorize]
    public class AmbienteController : BaseController
    {
        public AmbienteController(IConfiguration configuracao) : base(configuracao)
        {
        }            
    }
}
