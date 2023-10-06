using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]    
    public class AnexoController : BaseController
    {
        public AnexoController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }

        [HttpPost]
        [ClaimsAuthorize("AlterarTodosAnexos,AlterarMeusAnexos", "True")]
        public async Task<IActionResult> Salvar([FromForm] AnexoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                var resultadoCadastro = new Resultado<Anexo>();

                using (var stream = new MemoryStream())
                {
                    viewModel.FileUpload.CopyTo(stream);
                    resultadoCadastro = _aplicacao.Anexo.Cadastrar(viewModel.AnexoTipoId, viewModel.AnexoArquivoTipoId, viewModel.ObjetoId, stream, viewModel.FileUpload?.FileName, usuarioLogado, viewModel.ParametrosAdicionais,false);

                }               

                if (!resultadoCadastro.Sucesso)
                    resultado.Mensagem = resultadoCadastro.Mensagem;
                else
                {
                    _aplicacao.SaveChanges();
                    resultado.Sucesso = true;
                }
            }

            return Content(GerarRetorno(resultado), "application/json");
        }

        [HttpGet]        
        public IActionResult CarregarFormatosAceitos(int id)
        {
            var formatos = _aplicacao.ArquivoFormato.SelecionarTodos(af => af.AnexoArquivoTipos.Any(aat => aat.AnexoArquivoTipoId == id)).ToList();

            resultado.Sucesso = true;
            resultado.Retorno = string.Join(",", formatos.Select(f => f.Nome));

            return Content(GerarRetorno(resultado), "application/json");
        }        
    }
}
