using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AutenticacaoController : BaseController
    {
        public AutenticacaoController(IConfiguration configuracao) : base(configuracao)
        {
        }

        [AllowAnonymous]
        public IActionResult Login([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            var policySettings = _configuracao.GetSection("AzureAdB2C");
            var signUpSignInPolicyId = policySettings["SignUpSignInPolicyId"];
            properties.Items["policy"] = signUpSignInPolicyId;
            return Challenge(properties, scheme);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user from your application's authentication.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign out the user from Azure AD B2C.
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Login), "Autenticacao", null, Request.Scheme)
            };
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, authProperties);

            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
