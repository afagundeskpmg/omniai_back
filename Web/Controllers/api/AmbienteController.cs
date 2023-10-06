using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AmbienteController : BaseController
    {
        public AmbienteController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }       
    }
}
