using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ClaimsAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string[] _claimNames;
    private readonly string _claimValue;



    public ClaimsAuthorizeAttribute(string claimNames, string claimValue)
    {
        _claimNames = claimNames.Split(',');
        _claimValue = claimValue;
    }



    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var identity = user.Identity as System.Security.Claims.ClaimsIdentity;
        if (identity == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!user.Claims.Any(c => _claimNames.Any(cn => cn == c.Type)))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}