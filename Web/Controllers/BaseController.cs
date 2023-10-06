using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.Security.Claims;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWorkAplicacao _aplicacao;
        protected IConfiguration _configuracao;
        protected Usuario usuarioLogado;
        protected ClaimsIdentity _claims;
        private IConfiguration configuracao;
        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IConfiguration configuracao, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _configuracao = configuracao;
            _aplicacao = new UnitOfWorkAplicacao(configuracao);
            var user = contextAccessor.HttpContext.User;            
            
            if (user.Identity.IsAuthenticated)
            {
                var userEmail = user.Claims.FirstOrDefault(x => x.Type == "UserEmail").Value;
                usuarioLogado = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == userEmail);
            }

            if (usuarioLogado != null)
            {
                AdicionarClaimsIdentity();
                _claims = user.Identity as ClaimsIdentity;
            }
        }

        public BaseController(IConfiguration configuracao)
        {
            this.configuracao = configuracao;
        }

        private void AdicionarClaimsIdentity()
        {
            var user = _contextAccessor.HttpContext.User;
            var claims = usuarioLogado.Claims;

            if (claims != null && claims.Any())
            {
                var identity = (ClaimsIdentity)user.Identity;
                //Adiciona extensions como claims
                foreach (var claim in claims)
                {
                    if (!identity.Claims.Any(c => c.Type == claim.ClaimType))
                        identity.AddClaim(new System.Security.Claims.Claim(claim.ClaimType, claim.ClaimValue));
                }
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sistemaEmManutencao = SelecionarParametro(ParametroAmbienteEnum.Manutencao);
            if (sistemaEmManutencao == "true")
                filterContext.Result = new RedirectResult(Url.Action("Manutencao", "Home"));
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userEmail = User.Claims.FirstOrDefault(x => x.Type == "UserEmail").Value;
                    usuarioLogado = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == userEmail);
                }

                if (usuarioLogado != null)
                {
                    _claims = User.Identity as System.Security.Claims.ClaimsIdentity;
                    ViewBag.UsuarioNome = usuarioLogado.Nome;
                    ViewBag.UsuarioEmail = usuarioLogado.UserName;
                    ViewBag.UsuarioId = usuarioLogado.Id;
                    ViewBag.VersaoSistema = "1.0";
                    ViewBag.DarkMode = usuarioLogado.TemaConfiguracao?.DarkMode.ToString().ToLower() ?? "false";
                    ViewBag.LogoBg = usuarioLogado.TemaConfiguracao?.LogoBg ?? "skin1";
                    ViewBag.NavbarBg = usuarioLogado.TemaConfiguracao?.NavbarBg ?? "skin1";
                    ViewBag.SidebarColor = usuarioLogado.TemaConfiguracao?.SidebarColor ?? "skin1";
                }
            }

            SetarIdioma();
        }
        public void SetarIdioma(string idioma = null)
        {
            var culture = "pt-BR";
            var langCookie = Request.Cookies["lang"];

            if (langCookie != null)
                culture = langCookie;
            else if (usuarioLogado != null)
                culture = usuarioLogado.Ambiente.Cliente.Pais.CultureInfo;
            else if (!string.IsNullOrEmpty(idioma))
            {
                Response.Cookies.Append("lang", idioma);
                culture = idioma;
            }

            // Define o idioma atual da thread
            var cultureInfo = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        public bool PossuiClaims(params string[] claims)
        {
            foreach (var claim in claims)
            {
                if (_claims != null && _claims.FindFirst(c => c.Type == claim) != null)
                    return true;
            }
            return false;
        }
        protected string? SelecionarParametro(ParametroAmbienteEnum parametroAmbienteEnum)
        {
            return _configuracao[parametroAmbienteEnum.ToString()];
        }

        
    }
}