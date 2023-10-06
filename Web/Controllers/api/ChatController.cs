using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel.Projeto;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ChatController : BaseController
    {
        public ChatController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor) { }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosProjetos,AlterarMeusProjetos", "True")]
        public IActionResult Perguntar([FromForm] ChatPerguntarViewModel viewModel)
        {
            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                var projeto = _aplicacao.Projeto.SelecionarPorId(viewModel.ProjetoId);

                if (projeto == null)
                    resultado.Mensagem = "O projeto não foi localizado";
                else if (projeto != null && !usuarioLogado.PertenceAoAmbiente(projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos"))
                    resultado.Mensagem = "O projeto não pertence ao ambiente do usuários logado";
                else
                {
                    var perguntaResposta = new PerguntaResposta(projeto, viewModel.Pergunta, projeto.CaminhoBlobStorage);
                    _aplicacao.PerguntaResposta.Inserir(perguntaResposta);
                    _aplicacao.SaveChanges();

                    var resultadoPergunta = _aplicacao.PerguntaResposta.Perguntar(perguntaResposta);                    

                    if (!resultadoPergunta.Sucesso)
                        resultado.AtribuirMensagemErro(resultadoPergunta);
                    else
                    {
                        resultado.Retorno = resultadoPergunta.Retorno.Serializar();
                        resultado.Sucesso = true;
                        _aplicacao.SaveChanges();
                    }

                }
            }

            return AdicionarResultado(ref resultado);
        }
    }
}
