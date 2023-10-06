using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModel;
using Web.ViewModel.Projeto;

namespace Web.Controllers.api
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ProjetoController : BaseController
    {
        public ProjetoController(IConfiguration configuracao, IHttpContextAccessor contextAccessor) : base(configuracao, contextAccessor)
        {
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosProjetos,AlterarMeuProjetos", "True")]
        public IActionResult Cadastrar([FromForm] ProjetoCadastroViewModel viewModel)
        {
            var resultado = new Resultado<object>();

            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                if (viewModel.AmbienteId == null)
                    viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

                resultado = _aplicacao.Projeto.Cadastrar(viewModel.Nome, (int)viewModel.AmbienteId, usuarioLogado);
            }

            if (resultado.Sucesso)
                _aplicacao.SaveChanges();

            return AdicionarResultado(ref resultado);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosProjetos,AlterarMeuProjetos", "True")]
        public IActionResult Excluir(ExcluirRegistroViewModel viewModel)
        {
            var resultado = new Resultado<object>();
            var projeto = _aplicacao.Projeto.SelecionarPorId(viewModel.Id);

            if (projeto == null || (projeto != null && !usuarioLogado.PertenceAoAmbiente(projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos")))
                resultado.Mensagem = "O projeto solicitado não pertence à rede do usuário logado ou não foi encontrado. ID do Projeto: " + viewModel.Id;
            else
            {
                projeto.Excluido = viewModel.Excluir;
                projeto.AtribuirInformacoesRegistroParaAlteracao(usuarioLogado.Id);
                _aplicacao.Projeto.Atualizar(projeto);
                _aplicacao.SaveChanges();
                resultado.Sucesso = true;
            }


            return AdicionarResultado(ref resultado);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("VisualizarTodosProjetos,VisualizarMeuProjetos", "True")]
        public IActionResult ListarPorFiltro([FromForm] ProjetoListarViewModel viewModel)
        {
            if (!viewModel.AmbienteId.HasValue)
                viewModel.AmbienteId = usuarioLogado.Ambiente.Id;

            if (!usuarioLogado.PossuiClaim("VisualizarTodosProjetos") && !usuarioLogado.PertenceAoAmbiente(viewModel.AmbienteId.Value))
                resultadoSelecao.Mensagem = "Acesso negado para listar projetos no ambiente com ID: " + viewModel.AmbienteId;
            else
            {
                DateTime? dataDe = null;
                DateTime? dataAte = null;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataDeT))
                    dataDe = dataDeT;

                if (!string.IsNullOrEmpty(viewModel.DataInicial) && DateTime.TryParse(viewModel.DataInicial, out DateTime dataAteT))
                    dataAte = dataAteT;

                resultadoSelecao = _aplicacao.Projeto.SelecionarPorFiltro(viewModel.AmbienteId, viewModel.Nome, dataDe, dataAte, viewModel.Excluido, viewModel.Start, viewModel.Length);
            }

            return AdicionarResultado(ref resultadoSelecao);
        }
        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosAnexos,AlterarMeusAnexos,AlterarTodosProjetos,AlterarMeusProjetos,AlterarTodosDocumentosTipo,AlterarMeusDocumentosTipo", "True")]
        public async Task<IActionResult> EnviarArquivo([FromForm] UploadArquivoViewModel viewModel)
        {
            var resultadoProcessamentoArquivos = new Resultado<Object>() { };

            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                DocumentoTipo documentoTipo = null;
                var projeto = _aplicacao.Projeto.SelecionarPorId(viewModel.ProjetoId);

                if (viewModel.DocumentoTipoId != null)
                    documentoTipo = _aplicacao.DocumentoTipo.SelecionarPorId(viewModel.DocumentoTipoId);

                if (projeto == null || (projeto != null && !usuarioLogado.PertenceAoAmbiente(projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos")))
                    resultadoProcessamentoArquivos.Mensagem = "O projeto informado não pertence a rede do usuário logado";
                else if (documentoTipo != null && !usuarioLogado.PertenceAoAmbiente(documentoTipo.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosDocumentosTipo"))
                    resultadoProcessamentoArquivos.Mensagem = "O tipo de documento informado não pertence a rede do usuário logado";
                else if (viewModel.Arquivo == null)
                    resultadoProcessamentoArquivos.Mensagem = "O campo File é requerido";
                else
                {
                    var resultadoCadastro = new Resultado<Anexo>();
                    var resultadoCadastrarProcessamento = _aplicacao.ProcessamentoIndexer.CadastrarPorProjeto(projeto, usuarioLogado);

                    if (resultadoCadastrarProcessamento.Sucesso)
                    {
                        var projetoAnexo = new ProjetoAnexo(projeto, resultadoCadastrarProcessamento.Retorno, viewModel.DocumentoTipoId, usuarioLogado.Id);
                        if (resultadoCadastrarProcessamento.Retorno.Anexos == null)
                            resultadoCadastrarProcessamento.Retorno.Anexos = new List<ProjetoAnexo>();

                        resultadoCadastrarProcessamento.Retorno.Anexos.Add(projetoAnexo);

                        resultadoCadastro = _aplicacao.Anexo.Cadastrar((int)AnexoTipoEnum.Documento, (int)AnexoArquivoTipoEnum.Documento, projetoAnexo, viewModel.Arquivo.OpenReadStream(), viewModel.Arquivo.FileName, usuarioLogado, null, false);

                        if (!resultadoCadastro.Sucesso)
                            resultadoProcessamentoArquivos.Mensagem = resultadoCadastro.Mensagem;
                        else
                        {
                            _aplicacao.SaveChanges();

                            var processamento = projetoAnexo.ProcessamentoAnexo;
                            resultadoProcessamentoArquivos.Retorno = new { ProcessamentoId = processamento.Id, Anexo = projetoAnexo.Anexo.SerializarParaListar() };
                            resultadoProcessamentoArquivos.Sucesso = true;
                        }
                    }
                }
            }

            return AdicionarResultado(ref resultadoProcessamentoArquivos);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosAnexos,AlterarMeusAnexos,AlterarTodosProjetos,AlterarMeusProjetos,AlterarTodosDocumentosTipo,AlterarMeusDocumentosTipo", "True")]
        public IActionResult AtualizarDocumentoTipo([FromForm] ProjetoAtualizarDocumentoTipoPorAnexo viewModel)
        {
            if (!ModelState.IsValid)
                resultado.Mensagem = GerarErrosModelStateString();
            else
            {
                DocumentoTipo documentoTipo = _aplicacao.DocumentoTipo.SelecionarPorId(viewModel.DocumentoTipoId);
                var projetoAnexo = _aplicacao.ProjetoAnexo.SelecionarFirstOrDefault(x => x.ProjetoId == viewModel.ProjetoId && x.AnexoId == viewModel.AnexoId);

                if (projetoAnexo == null || (projetoAnexo != null && !usuarioLogado.PertenceAoAmbiente(projetoAnexo.Projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos")))
                    resultado.Mensagem = "O projeto informado não pertence a rede do usuário logado";
                else if (documentoTipo == null || (documentoTipo != null && !usuarioLogado.PertenceAoAmbiente(documentoTipo.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosDocumentosTipo")))
                    resultado.Mensagem = "O tipo de documento informado não pertence a rede do usuário logado";
                else
                {
                    projetoAnexo.DocumentoTipo = documentoTipo;
                    projetoAnexo.DocumentoTipoId = viewModel.DocumentoTipoId;
                    _aplicacao.ProjetoAnexo.Atualizar(projetoAnexo);
                    _aplicacao.SaveChanges();

                    resultado.Retorno = projetoAnexo.SerializarParaListar();
                    resultado.Sucesso = true;
                }
            }

            return AdicionarResultado(ref resultado);
        }
        [HttpPost]
        [ApiExplorerSettings(GroupName = "API")]
        [ClaimsAuthorize("AlterarTodosProjetos,AlterarMeusProjetos", "True")]
        public IActionResult IndexarArquivos([FromForm] ProjetoIndexarArquivosViewModel viewModel)
        {
            if (!ModelState.IsValid)
                AdicionarErrosModelStateResultado(ref resultado);
            else
            {
                var projeto = _aplicacao.Projeto.SelecionarPorId(viewModel.Id);

                if (projeto == null)
                    resultado.Mensagem = "O projeto não foi localizado";
                else if (projeto != null && !usuarioLogado.PertenceAoAmbiente(projeto.Ambiente.Id) && !usuarioLogado.PossuiClaim("AlterarTodosProjetos"))
                    resultado.Mensagem = "O projeto não pertence ao ambiente do usuário logado";
                else if (!projeto.ProcessamentosIndexers.Any(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessamentoSolicitado && x.LiberadoEm == null))
                    resultado.Mensagem = "O projeto não contem processamentos pendentes.";
                else
                {
                    var processamento = projeto.ProcessamentosIndexers.FirstOrDefault(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessamentoSolicitado && x.LiberadoEm == null);

                    if (processamento.Anexos.Any(x => x.ProcessamentoAnexo.ProcessamentoStatusId != (int)ProcessamentoStatusEnum.ProcessadoComSucesso && x.ProcessamentoAnexo.ProcessamentoStatusId != (int)ProcessamentoStatusEnum.ProcessadoComErro))
                        resultado.Mensagem = "Não é possivel inciar a indexação pois existem anexos em processamento";
                    else
                    {
                        processamento.Liberar(usuarioLogado);
                        var processamentoTemp = (Processamento)processamento;
                        var processamentoTipo = _aplicacao.ProcessamentoTipo.SelecionarPorId((int)ProcessamentoTipoEnum.Indexer);
                        _aplicacao.Processamento.AtribuirQueueProcessamento(ref processamentoTemp, _aplicacao.Processamento.SelecionarParametro(ParametroAmbienteEnum.StorageConnection), processamentoTipo.QueueNome, JsonConvert.SerializeObject(new { Id = processamento.Id, projeto.Ambiente.Cliente.Pais.CultureInfo }), 0);

                        _aplicacao.Processamento.Atualizar(processamento);
                        _aplicacao.SaveChanges();

                        resultado.Retorno = processamento.SerializarParaListar();
                        resultado.Sucesso = true;
                    }
                }
            }

            return AdicionarResultado(ref resultado);
        }
    }
}
