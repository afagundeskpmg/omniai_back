using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            Log.LogArquivo.Log(context.Exception, null);

            if (!context.ActionDescriptor.DisplayName.Contains("Controllers.api"))
                context.Result = new RedirectToActionResult("Erro", "Home", context.HttpContext.Request.Scheme);
            else
            {
                var resultado = new Resultado<DatatableRetorno<string>>();
                resultado.Sucesso = false;
                resultado.Mensagem = Multilingua.AlertasNotificacoes.AlertasNotificacoes.FuncaoIndisponivelNoMomento + "\n" + context.Exception.ToString();
                resultado.Retorno = new DatatableRetorno<string>() { data = new List<string>() };

                context.Result = new JsonResult(resultado) { StatusCode = 500 };
            }
        }
    }
}
