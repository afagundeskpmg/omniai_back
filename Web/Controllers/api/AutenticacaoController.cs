using Dominio.Entidades;
using Dominio.Util.API.B2C;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Route("api/[controller]/[action]")]
    public class AutenticacaoController : BaseController
    {       
        public AutenticacaoController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {            
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        public async Task<ActionResult> Token([FromForm] APIGetTokenViewModel model)
        {
            var resultado = new Resultado<Token>() { Retorno = new Token() };

            if (!ModelState.IsValid)
            {
                AdicionarErrosModelStateResultado(ref resultado);
                resultado.Retorno.error = resultado.Mensagem;
            }
            else
            {
                var usuario = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == model.username);

                if (usuario == null || (usuario != null && usuario.IdentityId == null))
                    resultado.Retorno.error = "Usuário sem acesso a plataforma";
                else if (usuario != null && usuario.Excluido)
                    resultado.Retorno.error = "Usuário bloqueado";
                else if (usuario != null && (usuario.Claims == null || (usuario.Claims != null && !usuario.Claims.Any())))
                    resultado.Retorno.error = "Usuário sem permissão ";
                else
                {
                    resultado = await _aplicacao.Usuario.SelecionarMicrosoftGrathToken(model.username, model.password);
                    if (resultado.Sucesso)
                    {
                        usuario.UltimoAcessoEm = DateTime.Now;
                        usuario.UltimoAcessoIP = _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

                        if (usuario.Interacoes == null)
                            usuario.Interacoes = new List<InteracaoUsuario>();

                        usuario.Interacoes.Add(new InteracaoUsuario(usuario.Id, string.Concat("API:", "Login relizado com suscesso"), /* Traduzir Resources.TextosAlertasNotificacoes.LoginRealizadoSucesso)*/ usuario.UltimoAcessoIP));

                        _aplicacao.Usuario.Atualizar(usuario);
                        _aplicacao.SaveChanges();
                    }
                }
            }

            return AdicionarResultado(ref resultado);

        }
    }
}
