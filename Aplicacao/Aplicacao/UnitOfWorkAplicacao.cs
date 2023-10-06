using Aplicacao.Interface;
using Aplicacao.Util;
using Dados.Contexto;
using Dados.Interface;
using Dados.Repositorio;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Aplicacao.Aplicacao
{
    public class UnitOfWorkAplicacao : IUnitOfWorkAplicacao, IDisposable
    {
        private readonly ContextoBase _contexto;
        private readonly IConfiguration _configuracao;
        private readonly IUnitOfWorkRepositorio _repositorio;

        public UnitOfWorkAplicacao(IConfiguration configuracao)
        {
            _contexto = new ContextoBase(configuracao);
            _configuracao = configuracao;
            _repositorio = new UnitOfWorkRepositorio(_contexto, _configuracao);
            PessoaJuridica = new PessoaJuridicaAplicacao(_repositorio, _configuracao);
            Pessoa = new PessoaAplicacao(_repositorio, _configuracao);
            ArquivoFormato = new ArquivoFormatoAplicacao(_repositorio, _configuracao);
            Ambiente = new AmbienteAplicacao(_repositorio, _configuracao);
            AnexoArquivoTipo = new AnexoArquivoTipoAplicacao(_repositorio, _configuracao);
            DocumentoTipo = new DocumentoTipoAplicacao(_repositorio, _configuracao);
            Projeto = new ProjetoAplicacao(_repositorio, _configuracao);
            Entidade = new EntidadeAplicacao(_repositorio, _configuracao);
            ProcessamentoStatus = new ProcessamentoStatusAplicacao(_repositorio, _configuracao);
            Processamento = new ProcessamentoAplicacao(_repositorio, _configuracao);
            Anexo = new AnexoAplicacao(_repositorio, _configuracao, Processamento);
            ProjetoAnexo = new ProjetoAnexoAplicacao(_repositorio, _configuracao);
            ProcessamentoIndexer = new ProcessamentoIndexerAplicacao(_repositorio, _configuracao, Processamento);
            ProcessamentoTipo = new ProcessamentoTipoAplicacao(_repositorio, _configuracao);
            UsuarioClaim = new UsuarioClaimAplicacao(_repositorio, _configuracao);
            Email = new EmailAplicacao(_repositorio, _configuracao, Processamento);
            Usuario = new UsuarioAplicacao(_repositorio, _configuracao, Email, UsuarioClaim);
            ProcessamentoAnexo = new ProcessamentoAnexoAplicacao(_repositorio, _configuracao, Anexo);
            ProcessamentoNer = new ProcessamentoNerAplicacao(_repositorio, _configuracao);            
            PerguntaResposta = new PerguntaRespostaAplicacao(_repositorio, _configuracao);
            ProcessamentoPergunta = new ProcessamentoPerguntaAplicacao(_repositorio, _configuracao, PerguntaResposta);
            //NAO REMOVER ESSA LINHA 1
        }

        public IAnexoAplicacao Anexo { get; private set; }
        public IPessoaJuridicaAplicacao PessoaJuridica { get; private set; }
        public IPessoaAplicacao Pessoa { get; private set; }
        public IArquivoFormatoAplicacao ArquivoFormato { get; private set; }
        public IUsuarioAplicacao Usuario { get; private set; }
        public IAmbienteAplicacao Ambiente { get; private set; }
        public IAnexoArquivoTipoAplicacao AnexoArquivoTipo { get; private set; }
        public IDocumentoTipoAplicacao DocumentoTipo { get; private set; }
        public IProjetoAplicacao Projeto { get; private set; }
        public IEntidadeAplicacao Entidade { get; private set; }
        public IProcessamentoStatusAplicacao ProcessamentoStatus { get; private set; }
        public IProjetoAnexoAplicacao ProjetoAnexo { get; private set; }
        public IProcessamentoIndexerAplicacao ProcessamentoIndexer { get; private set; }
        public IProcessamentoAplicacao Processamento { get; private set; }
        public IProcessamentoTipoAplicacao ProcessamentoTipo { get; private set; }
        public IUsuarioClaimAplicacao UsuarioClaim { get; private set; }
        public IEmailAplicacao Email { get; private set; }
        public IProcessamentoAnexoAplicacao ProcessamentoAnexo { get; private set; }
        public IProcessamentoNerAplicacao ProcessamentoNer { get; private set; }
        public IProcessamentoPerguntaAplicacao ProcessamentoPergunta { get; private set; }
        public IPerguntaRespostaAplicacao PerguntaResposta { get; private set; }
        //NAO REMOVER ESSA LINHA 2

        public int SaveChanges()
        {
            return _repositorio.SaveChanges();
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }

        public void ConfigurarEntidadesNoUnitOfWork(params string[] nomesDasEntidades)
        {
            /*Para rodar esse metodo e funcionar por completo deve ser chamado do projeto Teste para que os caminhos fiquem corretos*/
            string templateRepositorioInterface = AssemblyUtil.LerArquivoEmbutido("RepositorioInterface.txt", typeof(UnitOfWorkAplicacao).Assembly);
            string templateRepositorioImplementacao = AssemblyUtil.LerArquivoEmbutido("RepositorioImplementacao.txt", typeof(UnitOfWorkAplicacao).Assembly);
            string templateAplicacaoInterface = AssemblyUtil.LerArquivoEmbutido("AplicacaoInterface.txt", typeof(UnitOfWorkAplicacao).Assembly);
            string templateAplicacaoImplementacao = AssemblyUtil.LerArquivoEmbutido("AplicacaoImplementacao.txt", typeof(UnitOfWorkAplicacao).Assembly);

            string conteudoArquivoRepositorioInterface = string.Empty;
            string conteudoArquivoRepositorioImplementacao = string.Empty;
            string conteudoArquivoAplicacaoInterface = string.Empty;
            string conteudoArquivoAplicacaoImplementacao = string.Empty;

            string caminhoArquivoRepositorioInterface = string.Empty;
            string caminhoArquivoRepositorioImplementacao = string.Empty;
            string caminhoArquivoAplicacaoInterface = string.Empty;
            string caminhoArquivoAplicacaoImplementacao = string.Empty;

            string caminhoRaiz = Directory.GetParent(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName).FullName).FullName;
            caminhoRaiz = caminhoRaiz.Replace("\\Teste", "");

            string caminhoArquivoUnitOfWorkRepositorioInterface = Path.Combine(caminhoRaiz, "Dados", "Interface", "IUnitOfWorkRepositorio.cs");
            string caminhoArquivoUnitOfWorkRepositorioImplementacao = Path.Combine(caminhoRaiz, "Dados", "Repositorio", "UnitOfWorkRepositorio.cs");
            string caminhoArquivoUnitOfWorkAplicacaoInterface = Path.Combine(caminhoRaiz, "Aplicacao", "Interface", "IUnitOfWorkAplicacao.cs");
            string caminhoArquivoUnitOfWorkAplicacaoImplementacao = Path.Combine(caminhoRaiz, "Aplicacao", "Aplicacao", "UnitOfWorkAplicacao.cs");

            string conteudoArquivoUnitOfWorkRepositorioInterface = string.Empty;
            string conteudoArquivoUnitOfWorkRepositorioImplementacao = string.Empty;
            string conteudoArquivoUnitOfWorkAplicacaoInterface = string.Empty;
            string conteudoArquivoUnitOfWorkAplicacaoImplementacao = string.Empty;

            string caminhoArquivoProjetoDados = Path.Combine(caminhoRaiz, "Dados", "Dados.csproj");
            string caminhoArquivoProjetoAplicacao = Path.Combine(caminhoRaiz, "Aplicacao", "Aplicacao.csproj");

            string conteudoArquivoProjetoDados = string.Empty;
            string conteudoArquivoProjetoAplicacao = string.Empty;

            foreach (var nomeEntidade in nomesDasEntidades)
            {
                caminhoArquivoRepositorioInterface = Path.Combine(caminhoRaiz, "Dados", "Interface", "I" + nomeEntidade + "Repositorio.cs");
                caminhoArquivoRepositorioImplementacao = Path.Combine(caminhoRaiz, "Dados", "Repositorio", nomeEntidade + "Repositorio.cs");
                caminhoArquivoAplicacaoInterface = Path.Combine(caminhoRaiz, "Aplicacao", "Interface", "I" + nomeEntidade + "Aplicacao.cs");
                caminhoArquivoAplicacaoImplementacao = Path.Combine(caminhoRaiz, "Aplicacao", "Aplicacao", nomeEntidade + "Aplicacao.cs");

                conteudoArquivoRepositorioInterface = templateRepositorioInterface.Replace("#ENTITY_NAME#", nomeEntidade);
                conteudoArquivoRepositorioImplementacao = templateRepositorioImplementacao.Replace("#ENTITY_NAME#", nomeEntidade);
                conteudoArquivoAplicacaoInterface = templateAplicacaoInterface.Replace("#ENTITY_NAME#", nomeEntidade);
                conteudoArquivoAplicacaoImplementacao = templateAplicacaoImplementacao.Replace("#ENTITY_NAME#", nomeEntidade);

                conteudoArquivoUnitOfWorkRepositorioInterface = File.ReadAllText(caminhoArquivoUnitOfWorkRepositorioInterface);
                conteudoArquivoUnitOfWorkRepositorioImplementacao = File.ReadAllText(caminhoArquivoUnitOfWorkRepositorioImplementacao);
                conteudoArquivoUnitOfWorkAplicacaoInterface = File.ReadAllText(caminhoArquivoUnitOfWorkAplicacaoInterface);
                conteudoArquivoUnitOfWorkAplicacaoImplementacao = File.ReadAllText(caminhoArquivoUnitOfWorkAplicacaoImplementacao);

                conteudoArquivoProjetoDados = File.ReadAllText(caminhoArquivoProjetoDados);
                conteudoArquivoProjetoAplicacao = File.ReadAllText(caminhoArquivoProjetoAplicacao);

                if (!File.Exists(caminhoArquivoRepositorioInterface))
                    File.WriteAllText(caminhoArquivoRepositorioInterface, conteudoArquivoRepositorioInterface);
                if (!File.Exists(caminhoArquivoRepositorioImplementacao))
                    File.WriteAllText(caminhoArquivoRepositorioImplementacao, conteudoArquivoRepositorioImplementacao);
                if (!File.Exists(caminhoArquivoAplicacaoInterface))
                    File.WriteAllText(caminhoArquivoAplicacaoInterface, conteudoArquivoAplicacaoInterface);
                if (!File.Exists(caminhoArquivoAplicacaoImplementacao))
                    File.WriteAllText(caminhoArquivoAplicacaoImplementacao, conteudoArquivoAplicacaoImplementacao);

                var variavelRepInterface = "I" + nomeEntidade + "Repositorio " + nomeEntidade + " { get; }";
                var variavelRep = "public I" + nomeEntidade + "Repositorio " + nomeEntidade + " { get; private set; }";
                var variavelRepPopular = nomeEntidade + " = new " + nomeEntidade + "Repositorio(_contexto);";

                var variavelAppInterface = "I" + nomeEntidade + "Aplicacao " + nomeEntidade + " { get; }";
                var variavelApp = "public I" + nomeEntidade + "Aplicacao " + nomeEntidade + " { get; private set; }";
                var variavelAppPopular = nomeEntidade + " = new " + nomeEntidade + "Aplicacao(_repositorio, _configuracao);";

                conteudoArquivoUnitOfWorkRepositorioInterface = conteudoArquivoUnitOfWorkRepositorioInterface.Contains(variavelRepInterface) ?
                                                                conteudoArquivoUnitOfWorkRepositorioInterface :
                                                                ReplaceFirst(conteudoArquivoUnitOfWorkRepositorioInterface, "//NAO REMOVER ESSA LINHA 1", variavelRepInterface + Environment.NewLine + "        //NAO REMOVER ESSA LINHA 1");

                conteudoArquivoUnitOfWorkRepositorioImplementacao = conteudoArquivoUnitOfWorkRepositorioImplementacao.Contains(variavelRep) ?
                                                                    conteudoArquivoUnitOfWorkRepositorioImplementacao :
                                                                    ReplaceFirst(conteudoArquivoUnitOfWorkRepositorioImplementacao, "//NAO REMOVER ESSA LINHA 2", variavelRep + Environment.NewLine + "        //NAO REMOVER ESSA LINHA 2");

                conteudoArquivoUnitOfWorkRepositorioImplementacao = conteudoArquivoUnitOfWorkRepositorioImplementacao.Contains(variavelRepPopular) ?
                                                                    conteudoArquivoUnitOfWorkRepositorioImplementacao :
                                                                    ReplaceFirst(conteudoArquivoUnitOfWorkRepositorioImplementacao, "//NAO REMOVER ESSA LINHA 1", variavelRepPopular + Environment.NewLine + "            //NAO REMOVER ESSA LINHA 1");


                conteudoArquivoUnitOfWorkAplicacaoInterface = conteudoArquivoUnitOfWorkAplicacaoInterface.Contains(variavelAppInterface) ?
                                                              conteudoArquivoUnitOfWorkAplicacaoInterface :
                                                              ReplaceFirst(conteudoArquivoUnitOfWorkAplicacaoInterface, "//NAO REMOVER ESSA LINHA 1", variavelAppInterface + Environment.NewLine + "        //NAO REMOVER ESSA LINHA 1");

                conteudoArquivoUnitOfWorkAplicacaoImplementacao = conteudoArquivoUnitOfWorkAplicacaoImplementacao.Contains(variavelApp) ?
                                                                  conteudoArquivoUnitOfWorkAplicacaoImplementacao :
                                                                  ReplaceFirst(conteudoArquivoUnitOfWorkAplicacaoImplementacao, "//NAO REMOVER ESSA LINHA 2", variavelApp + Environment.NewLine + "        //NAO REMOVER ESSA LINHA 2");

                conteudoArquivoUnitOfWorkAplicacaoImplementacao = conteudoArquivoUnitOfWorkAplicacaoImplementacao.Contains(variavelAppPopular) ?
                                                                  conteudoArquivoUnitOfWorkAplicacaoImplementacao :
                                                                  ReplaceFirst(conteudoArquivoUnitOfWorkAplicacaoImplementacao, "//NAO REMOVER ESSA LINHA 1", variavelAppPopular + Environment.NewLine + "            //NAO REMOVER ESSA LINHA 1");

                File.WriteAllText(caminhoArquivoUnitOfWorkRepositorioInterface, conteudoArquivoUnitOfWorkRepositorioInterface);
                File.WriteAllText(caminhoArquivoUnitOfWorkRepositorioImplementacao, conteudoArquivoUnitOfWorkRepositorioImplementacao);
                File.WriteAllText(caminhoArquivoUnitOfWorkAplicacaoInterface, conteudoArquivoUnitOfWorkAplicacaoInterface);
                File.WriteAllText(caminhoArquivoUnitOfWorkAplicacaoImplementacao, conteudoArquivoUnitOfWorkAplicacaoImplementacao);

                conteudoArquivoProjetoDados = conteudoArquivoProjetoDados.Contains(nomeEntidade + "Repositorio.cs") ? conteudoArquivoProjetoDados : conteudoArquivoProjetoDados.Replace("<Compile Include=\"Repositorio\\BaseRepositorio.cs\" />", "<Compile Include=\"Repositorio\\BaseRepositorio.cs\" />" + Environment.NewLine + "    " + "<Compile Include=\"Repositorio\\" + nomeEntidade + "Repositorio.cs\" />");
                conteudoArquivoProjetoDados = conteudoArquivoProjetoDados.Contains("I" + nomeEntidade + "Repositorio.cs") ? conteudoArquivoProjetoDados : conteudoArquivoProjetoDados.Replace("<Compile Include=\"Interface\\IBaseRepositorio.cs\" />", "<Compile Include=\"Interface\\IBaseRepositorio.cs\" />" + Environment.NewLine + "    " + "<Compile Include=\"Interface\\I" + nomeEntidade + "Repositorio.cs\" />");

                conteudoArquivoProjetoAplicacao = conteudoArquivoProjetoAplicacao.Contains(nomeEntidade + "Aplicacao.cs") ? conteudoArquivoProjetoAplicacao : conteudoArquivoProjetoAplicacao.Replace("<Compile Include=\"Aplicacao\\BaseAplicacao.cs\" />", "<Compile Include=\"Aplicacao\\BaseAplicacao.cs\" />" + Environment.NewLine + "    " + "<Compile Include=\"Aplicacao\\" + nomeEntidade + "Aplicacao.cs\" />");
                conteudoArquivoProjetoAplicacao = conteudoArquivoProjetoAplicacao.Contains("I" + nomeEntidade + "Aplicacao.cs") ? conteudoArquivoProjetoAplicacao : conteudoArquivoProjetoAplicacao.Replace("<Compile Include=\"Interface\\IBaseAplicacao.cs\" />", "<Compile Include=\"Interface\\IBaseAplicacao.cs\" />" + Environment.NewLine + "    " + "<Compile Include=\"Interface\\I" + nomeEntidade + "Aplicacao.cs\" />");

                File.WriteAllText(caminhoArquivoProjetoDados, conteudoArquivoProjetoDados);
                File.WriteAllText(caminhoArquivoProjetoAplicacao, conteudoArquivoProjetoAplicacao);
            }
        }

        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
