using Aplicacao.APIs;
using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Dominio.Util.API.B2C;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class UsuarioAplicacao : BaseAplicacao<Usuario>, IUsuarioAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IEmailAplicacao _emailAplicacao;
        private readonly IUsuarioClaimAplicacao _usuarioClaimAplicacao;
        public UsuarioAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao, IEmailAplicacao emailAplicacao, IUsuarioClaimAplicacao usuarioClaimAplicacao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
            _emailAplicacao = emailAplicacao;
            _usuarioClaimAplicacao = usuarioClaimAplicacao;
        }
        
        public Resultado<DatatableRetorno<object>> SelecionarUsuariosPorFiltro(int[] ambientesIds, string nome, string email, bool? excluido, int? start, int? length)
        {
            return _repositorio.Usuario.SelecionarUsuariosPorFiltro(ambientesIds, nome, email, excluido, start, length);
        }
        public Resultado<object> DesativarUsuario(Usuario usuario)
        {
            usuario.Excluido = true;

            var resultado = SalvarClaimsUsuario(usuario, usuario, null);

            if (resultado.Sucesso)
                Atualizar(usuario);
            else
                resultado.Mensagem = string.Concat("Não foi possível desativar este usuário Error:", resultado.Mensagem);

            return resultado;
        }
        public Resultado<object> ReativarUsuario(Usuario usuario)
        {
            usuario.Excluido = false;

            var resultado = SalvarClaimsUsuario(usuario, usuario, usuario.Papel.Claims.Select(c => c.ClaimId).ToArray());

            if (resultado.Sucesso)
                Atualizar(usuario);
            else
                resultado.Mensagem = string.Concat("Não foi possível ativar este usuário", " Error:", resultado.Mensagem);

            return resultado;
        }
        public async Task<Resultado<Usuario>> Salvar(Ambiente ambiente, Usuario usuarioLogado, Papel papel, bool permitirAlterarEmpresa, string email, string nome)
        {
            var resultado = new Resultado<Usuario>();

            if (papel.Id == "43e9fd1c-a4f2-4165-9cee-3edb4931f1af")//sistema
            {
                if (email.Split('@')[1] != "watch.kpmg.com.br")
                    resultado.Mensagem = "Usuários de sistema precisam ser criados com o domínio @truevalue.kpmg.com.br";
                else
                {
                    var usuariosAutorizadosCriarSistema = new List<string>() { "djusti@kpmg.com.br", "viniciusmendes@kpmg.com.br", "jpoliveira1@kpmg.com.br" };
                    if (!usuariosAutorizadosCriarSistema.Contains(usuarioLogado.UserName))
                        resultado.Mensagem = "Contate um usuário autorizado para realizar o cadastro do usuário com o perfil \"Sistema\"";
                }
            }

            if (string.IsNullOrEmpty(resultado.Mensagem))
            {
                if (ambiente.Usuarios == null)
                    ambiente.Usuarios = new List<Usuario>();

                var usuario = SelecionarFirstOrDefault(x => x.UserName == email);

                if (usuario != null)
                {
                    usuario.AtribuirInformacoesRegistroParaAlteracao(usuarioLogado.Id);
                    Atualizar(usuario);
                }
                else
                {
                    usuario = new Usuario();
                    usuario.TemaConfiguracao = new TemaConfiguracao();
                    usuario.AtribuirInformacoesRegistroParaInsercao(usuarioLogado.Id);
                    Inserir(usuario);
                }

                if (!permitirAlterarEmpresa && usuario.Ambientes != null && usuario.Ambientes.Any(o => o.Id != ambiente.Id))
                    resultado.Mensagem = string.Format("Usuário de email {0} já cadastrado", email);
                else if (!ambiente.Usuarios.Any(u => u.Id == usuario.Id) && ambiente.QuantidadeMaximaUsuariosAtivos <= ambiente.Usuarios.Count(u => !u.Excluido && !u.PossuiClaims("AlterarTodosUsuarios")) && !usuario.PossuiClaims("AlterarTodosUsuarios"))
                    resultado.Mensagem = string.Format("Limite de usuário atingido {0}", ambiente.QuantidadeMaximaUsuariosAtivos);
                else
                {
                    usuario.Nome = nome;
                    usuario.UserName = email;
                    usuario.Excluido = false;
                    usuario.Federado = usuario.UserName.ToUpper().Contains("@KPMG.") == true;
                    usuario.Ambientes.Clear();
                    usuario.Ambientes.Add(ambiente);

                    var senhaTemp = GerarSenhaAleatoriaB2C(8);

                    //Cadastrar Usuário no B2C
                    if (string.IsNullOrEmpty(usuario.IdentityId))
                    {
                        var resultadoCadastroB2C = await CadastrarUsuarioB2C(usuario, senhaTemp);

                        if (resultadoCadastroB2C.Sucesso)
                        {
                            usuario.IdentityId = resultadoCadastroB2C.Retorno;
                            usuario.IdentityIdGeradoLocalmente = false;
                        }
                        else if (!resultadoCadastroB2C.Sucesso && !string.IsNullOrEmpty(resultadoCadastroB2C.Retorno))
                        {
                            usuario.IdentityId = resultadoCadastroB2C.Retorno;
                            usuario.IdentityIdGeradoLocalmente = true;
                        }
                        else
                            resultado.Mensagem = resultadoCadastroB2C.Mensagem;
                    }

                    if (string.IsNullOrEmpty(resultado.Mensagem))
                    {
                        if (usuario.Papel == null || papel.Id != usuario.PapelId)
                        {
                            usuario.Papel = papel;

                            _usuarioClaimAplicacao.DeletarTodos(usuario.Claims);

                            foreach (var claim in usuario.Papel.Claims)
                                usuario.Claims.Add(new UsuarioClaim(claim.Claim.ClaimType, claim.Claim.DefaultValue));

                            usuario.Interacoes.Add(new InteracaoUsuario(usuarioLogado, string.Format("Permissões estabelecidas para o papel: ", papel.Nome), null));
                        }



                        //se for um usuário de sistema, retornará a senha
                        if (usuario.PapelId == "43e9fd1c-a4f2-4165-9cee-3edb4931f1af")
                        {
                            var destinaraiosEmails = SelecionarParametro(ParametroAmbienteEnum.DestinatariosUsuariosSistema).Split(';');
                            var destinatarios = SelecionarTodos(u => destinaraiosEmails.Contains(u.UserName)).ToList();
                            if (destinatarios.Any())
                                _emailAplicacao.SalvarEmailParaUsuarios(destinatarios, "Novo usuário de sistema " + usuario.UserName, senhaTemp, usuarioLogado.Id, null);

                            resultado.Mensagem = "Anote a senha gerada, não será possível sua recuperação: " + senhaTemp;
                        }

                        if (!usuario.EmailBoasVindasEnviado)
                        {
                            var nomeTemplate = usuario.Federado ? "BoasVindasFederado{0}.html" : "BoasVindasNaoFederado{0}.html";
                            var cultureInfo = usuario.Ambiente.Cliente.Pais.CultureInfo == "es-ES" ? "en-US" : Thread.CurrentThread.CurrentCulture.ToString();
                            nomeTemplate = string.Format(nomeTemplate, cultureInfo);
                            var corpo = LerArquivoEmbutido(nomeTemplate, typeof(IUnitOfWorkAplicacao).Assembly);

                            corpo = corpo.Replace("#USUARIO_NOME#", usuario.Nome);
                            corpo = corpo.Replace("#TRUEVALUE_URL_LOGIN#", ComporURL("Login", "Index", null));
                            corpo = corpo.Replace("#USUARIO_EMAIL#", usuario.UserName);
                            corpo = corpo.Replace("#URL_PLATAFORMA#", SelecionarParametro(ParametroAmbienteEnum.URLPadrao));

                            _emailAplicacao.SalvarEmailParaUsuarios(new List<Usuario> { usuario }, string.Format("Bem vindo a platáforma ", "Omni A.I"), corpo, usuarioLogado.Id, null);

                            usuario.EmailBoasVindasEnviado = true;
                            //Atualizar(usuario);
                        }

                        resultado.Sucesso = true;
                        resultado.Retorno = usuario;
                    }


                }
            }

            return resultado;
        }
        public Resultado<object> SalvarClaimsUsuario(Usuario usuario, Usuario usuarioLogado, int[] claimsSelecionadasIds)
        {
            var resultado = new Resultado<object>();

            usuario.AtribuirInformacoesRegistroParaAlteracao(usuarioLogado.Id);

            var claimsExistentes = _repositorio.Repository<Claim>().SelecionarTodos().ToList();

            var claimsAnteriores = new List<Claim>();
            if (usuario.Claims != null && usuario.Claims.Any())
                claimsAnteriores.AddRange(claimsExistentes.Where(c => usuario.Claims.Select(cu => cu.ClaimType).Contains(c.ClaimType)).ToList());

            var claimsAdicionadas = new List<Claim>();

            _repositorio.UsuarioClaim.DeletarTodos(usuario.Claims);

            if (claimsSelecionadasIds != null && claimsSelecionadasIds.Any())
            {
                foreach (var claimId in claimsSelecionadasIds)
                {
                    var claim = claimsExistentes.FirstOrDefault(c => c.Id == claimId);
                    if (claim != null)
                    {
                        usuario.Claims.Add(new UsuarioClaim(claim.ClaimType, claim.DefaultValue));
                        if (!claimsAnteriores.Any(c => c.Id == claim.Id))
                            claimsAdicionadas.Add(claim);
                    }
                }
            }

            var claimsRemovidas = claimsAnteriores.Where(cu => !usuario.Claims.Any(c => c.ClaimType == cu.ClaimType));

            var interacoes = new List<string>();

            if (claimsRemovidas.Any())
            {
                var interacaoDesc = string.Format("Permissões removidas", " ");
                foreach (var claimRemovida in claimsRemovidas)
                {
                    if (interacaoDesc.Count() + claimRemovida.Descricao.Count() > 1500)
                    {
                        interacoes.Add(interacaoDesc);
                        interacaoDesc = string.Format("Permissões removidas", " ");
                    }
                    interacaoDesc += claimRemovida.Descricao + ", ";
                }

                interacoes.Add(interacaoDesc);
            }

            if (claimsAdicionadas.Any())
            {
                var interacaoDesc = string.Format("Permissões adicionadas", " ");
                foreach (var claimAdicionada in claimsAdicionadas)
                {
                    if (interacaoDesc.Count() + claimAdicionada.Descricao.Count() > 1500)
                    {
                        interacoes.Add(interacaoDesc);
                        interacaoDesc = string.Format("Permissões adicionadas", " ");
                    }
                    interacaoDesc += claimAdicionada.Descricao + ", ";
                }

                interacoes.Add(interacaoDesc);
            }

            if (interacoes.Any())
            {
                foreach (var interacaoDesc in interacoes)
                {
                    usuario.Interacoes.Add(new InteracaoUsuario(usuarioLogado, interacaoDesc, null));
                }
            }

            Atualizar(usuario);
            resultado.Sucesso = true;


            return resultado;
        }        
        private async Task<Resultado<string>> CadastrarUsuarioB2C(Usuario usuario, string senhaTemp)
        {
            var resultado = new Resultado<string>();

            var uri = string.Format(SelecionarParametro(ParametroAmbienteEnum.B2CFunctionURL), SelecionarParametro(ParametroAmbienteEnum.B2CFunctionCreatUsers));
            var body = JsonConvert.SerializeObject(new { Name = usuario.Nome, Email = usuario.UserName, Password = senhaTemp, Federed = usuario.Federado });

            var result = await FazerRequisicao(RestSharp.Method.Post, uri, body, "application/json");

            var idB2C = JsonConvert.DeserializeObject<RetornoB2C>(result.Content);

            if (!string.IsNullOrEmpty(idB2C?.id))
            {
                resultado.Retorno = idB2C.id;
                resultado.Sucesso = true;
            }
            else if (!string.IsNullOrEmpty(idB2C?.message) && idB2C.message.ToUpper().Contains("JÁ CADASTRADO"))
                resultado.Retorno = idB2C?.id ?? Guid.NewGuid().ToString();
            else if (!result.IsSuccessful)
                resultado.Mensagem = string.Format("Não foi possível cadastrar o usuário no B2C", result.ErrorMessage + " " + idB2C?.message);

            return resultado;

        }
        public async Task<Resultado<Token>> SelecionarMicrosoftGrathToken(string username, string password)
        {
            #region Variaveis             
            var resultado = new Resultado<Token>() { Retorno = new Token() };
            var tenant = SelecionarParametro(ParametroAmbienteEnum.B2CTenantName);
            var clientId = SelecionarParametro(ParametroAmbienteEnum.B2CClientId);
            var scope = clientId;

            var uri = String.Format(SelecionarParametro(ParametroAmbienteEnum.B2CAPIMicrosoftB2COAuthToken), tenant, SelecionarParametro(ParametroAmbienteEnum.B2CAPISignInPolicyId));
            var parametros = new List<Parameter>()
            {
                new Parameter("username", username),
                new Parameter("password", password),
                new Parameter("grant_type", "password"),
                new Parameter("client_id", clientId),
                new Parameter("scope", scope),
                new Parameter("response_type", "token")
            };
            #endregion

            try
            {
                var request = await FazerRequisicao(RestSharp.Method.Post, uri, parametros, "application/x-www-form-urlencoded");

                if (!request.IsSuccessful && request.Content?.Contains("error") == true)
                    resultado.Retorno = JsonConvert.DeserializeObject<Token>(request.Content.ToString());
                else if (!request.IsSuccessful)
                    resultado.Retorno.error = request.ErrorMessage;
                else
                {
                    resultado.Retorno = JsonConvert.DeserializeObject<Token>(request.Content);
                    resultado.Sucesso = true;
                }

            }
            catch (Exception ex)
            {
                resultado.MensagemInterna = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                resultado.Retorno.error = "Falha ao obter o token";
            }

            return resultado;
        }
        
    }
}
