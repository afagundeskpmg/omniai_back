using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Dominio.Entidades;
using Dominio.Util.API.B2C;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Security.Claims;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected IUnitOfWorkAplicacao _aplicacao;
        protected IConfiguration _configuracao;
        protected Usuario usuarioLogado;
        protected Resultado<DatatableRetorno<object>> resultadoSelecao;
        protected Resultado<object> resultado;
        protected ClaimsIdentity _claims;
        private IConfiguration configuracao;
        public readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IConfiguration configuracao, IHttpContextAccessor contextAccessor)
        {
            #region Parametros
            this._contextAccessor = contextAccessor;
            _configuracao = configuracao;
            _aplicacao = new UnitOfWorkAplicacao(configuracao);
            var user = contextAccessor.HttpContext.User;
            resultadoSelecao = new Resultado<DatatableRetorno<object>>() { Retorno = new DatatableRetorno<object>() { data = new List<object>() } };
            resultado = new Resultado<object>();
            #endregion

            if (user.Identity.IsAuthenticated)
            {
                var userEmail = user.Claims.FirstOrDefault(x => x.Type == "UserEmail").Value;
                usuarioLogado = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == userEmail);
                _claims = user.Identity as ClaimsIdentity;
            }
        }

        #region Metodos Privados       
        protected ObjectResult AdicionarResultado<t>(ref Resultado<t> result)
        {
            if (result.Sucesso)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult(result);
            
        }
        protected ObjectResult AdicionarResultado(ref Resultado<object> result)
        {
            if (result.Sucesso)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult(result);

        }
        protected string GerarErrosModelStateString()
        {
            return string.Join("; ", ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage));
        }
        protected string GerarRetorno(object retorno)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver(),
                Formatting = Formatting.None // ou Formatting.Indented para formato indentado
            };

            return JsonConvert.SerializeObject(retorno, jsonSettings);
        }
        [HttpGet("{idioma}")]
        private void SetarIdioma([FromRoute] string? idioma = null)
        {
            var culture = "pt-BR";
            var langCookie = Request.Cookies["lang"];

            if (langCookie != null)
                culture = langCookie;
            //else if (usuarioLogado != null)
            //    culture = usuarioLogado.Ambiente.Pais.CultureInfo ?? culture;
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
        protected bool PossuiClaims(params string[] claims)
        {
            foreach (var claim in claims)
            {
                if (_claims != null && _claims.FindFirst(c => c.Type == claim) != null)
                    return true;
            }
            return false;
        }
        protected void AdicionarErrosModelStateResultado<t>(ref Resultado<t> result)
        {
            result.Sucesso = false;
            result.Mensagem = string.Join("; ", ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage));
        }
        #endregion
    }
}
