using System.Security.Claims;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Validacoes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Web.Providers;

public class CustomIdentityValidation
{
    private readonly RequestDelegate _next;    

    public CustomIdentityValidation(RequestDelegate next)
    {
        _next = next;
    }
    public Task Invoke(HttpContext context)
    {        
        if (!context.Request.Path.Value.Contains("Autenticacao/Token") && !context.Request.Path.Value.ToLower().Contains("/health"))
        {
            if (context.User.Identity.IsAuthenticated)
                return OnTokenValidatedFunc(context);
            else
            {
                var claimPrincipal = context.User.Claims.FirstOrDefault(x => x.Type == "email");
                var userEmail = claimPrincipal != null ? claimPrincipal?.Value : context.User.Claims.FirstOrDefault(x => x.Type == "signInNames.emailAddress")?.Value;
                var resultRegisterAttemps = SessionManager.RegisterAttempts(userEmail);

                return ReturnError(context, StatusCodes.Status403Forbidden, "Autenticação necessária para acessar esta API ou aplicação MVC.");
            }
        }
        else
            return _next(context);
    }
    private Task OnTokenValidatedFunc(HttpContext context)
    {
        //Seleciona o email do usuário logado 
        var claimPrincipal = context.User.Claims.FirstOrDefault(x => x.Type == "email");
        var userEmail = claimPrincipal != null ? claimPrincipal?.Value : context.User.Claims.FirstOrDefault(x => x.Type == "signInNames.emailAddress")?.Value;
        var claimIdentity = context.User.Identity as ClaimsIdentity;

        //Valida se o usuário esta cadastrado localmente
        var resultValidation = SessionManager.UserIsValid(userEmail);

        if (!resultValidation.Sucesso)
            return  ReturnError(context, StatusCodes.Status401Unauthorized, resultValidation.Mensagem);
        else
        {
            //Seleciona as claims local do usuário e adiciona ao identity                
            var resultado = SessionManager.SelectUserClaims(userEmail);
            if (!resultado.Sucesso || (resultado.Sucesso && resultado.Retorno == null))
               return ReturnError(context, StatusCodes.Status401Unauthorized, "Este usuário não tem permissões cadastradas, entre em contato com um adiministrador");
            else
            {
                //Selecionar IP e User agent do usuário para controle de sessão
                var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
                var userAgent = context.Request.Headers["User-Agent"][0];

                //Gerar log de autenticação
                SessionManager.UpdateUserAuthLog(userEmail, remoteIpAddress);

                //Associa as claims locais , ip e agente ao identity
                resultado.Retorno.ForEach(x => claimIdentity.AddClaim(new System.Security.Claims.Claim(x.ClaimType, x.ClaimValue)));
                claimIdentity.AddClaim(new System.Security.Claims.Claim("ip_address", remoteIpAddress));
                claimIdentity.AddClaim(new System.Security.Claims.Claim("user-agent", userAgent));
                claimIdentity.AddClaim(new System.Security.Claims.Claim("UserEmail", userEmail));
            }
        }

        return _next(context);
    }
    private Task ReturnError(HttpContext context, int statuscodes, string message)
    {
        context.Response.StatusCode = statuscodes;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            message = message
        };

        var jsonResponse = JsonConvert.SerializeObject(errorResponse);
        return context.Response.WriteAsync(jsonResponse);
    }
}