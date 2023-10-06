using Dados.Contexto;
using Dados.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dados.Repositorio
{
    public class UnitOfWorkRepositorio : IUnitOfWorkRepositorio
    {
        private readonly ContextoBase _contexto;
        private readonly IConfiguration _configuracao;

        public UnitOfWorkRepositorio(ContextoBase contexto, IConfiguration configuracao)
        {
            _contexto = contexto;
            _configuracao = configuracao;
            PessoaJuridica = new PessoaJuridicaRepositorio(_contexto);
            Pessoa = new PessoaRepositorio(_contexto);
            Anexo = new AnexoRepositorio(_contexto);
            ArquivoFormato = new ArquivoFormatoRepositorio(_contexto);            
            Usuario = new UsuarioRepositorio(_contexto);
            Ambiente = new AmbienteRepositorio(_contexto);
            AnexoArquivoTipo = new AnexoArquivoTipoRepositorio(_contexto);                        
            DocumentoTipo = new DocumentoTipoRepositorio(_contexto);
            Projeto = new ProjetoRepositorio(_contexto);
            Entidade = new EntidadeRepositorio(_contexto);
            ProcessamentoStatus = new ProcessamentoStatusRepositorio(_contexto);
            ProjetoAnexo = new ProjetoAnexoRepositorio(_contexto);
            ProcessamentoIndexer = new ProcessamentoIndexerRepositorio(_contexto);            
            Processamento = new ProcessamentoRepositorio(_contexto);
            ProcessamentoTipo = new ProcessamentoTipoRepositorio(_contexto);
            UsuarioClaim = new UsuarioClaimRepositorio(_contexto);
            Email = new EmailRepositorio(_contexto);
            ProcessamentoAnexo = new ProcessamentoAnexoRepositorio(_contexto);
            ProcessamentoNer = new ProcessamentoNerRepositorio(_contexto);
            ProcessamentoPergunta = new ProcessamentoPerguntaRepositorio(_contexto);
            PerguntaResposta = new PerguntaRespostaRepositorio(_contexto);
            //NAO REMOVER ESSA LINHA 1
        }

        #region Repositorios             
        public IBaseRepositorio<T> Repository<T>() where T : class
        {
            return new BaseRepositorio<T>(_contexto);
        }
        public IPessoaJuridicaRepositorio PessoaJuridica { get; private set; }
        public IPessoaRepositorio Pessoa { get; private set; }
        public IAnexoRepositorio Anexo { get; private set; }
        public IArquivoFormatoRepositorio ArquivoFormato { get; private set; }        
        public IUsuarioRepositorio Usuario { get; private set; }
        public IAmbienteRepositorio Ambiente { get; private set; }
        public IAnexoArquivoTipoRepositorio AnexoArquivoTipo { get; private set; }                
        public IDocumentoTipoRepositorio DocumentoTipo { get; private set; }
        public IProjetoRepositorio Projeto { get; private set; }
        public IEntidadeRepositorio Entidade { get; private set; }
        public IProcessamentoStatusRepositorio ProcessamentoStatus { get; private set; }
        public IProjetoAnexoRepositorio ProjetoAnexo { get; private set; }
        public IProcessamentoIndexerRepositorio ProcessamentoIndexer { get; private set; }        
        public IProcessamentoRepositorio Processamento { get; private set; }
        public IProcessamentoTipoRepositorio ProcessamentoTipo { get; private set; }
        public IUsuarioClaimRepositorio UsuarioClaim { get; private set; }
        public IEmailRepositorio Email { get; private set; }
        public IProcessamentoAnexoRepositorio ProcessamentoAnexo { get; private set; }
        public IProcessamentoNerRepositorio ProcessamentoNer { get; private set; }
        public IProcessamentoPerguntaRepositorio ProcessamentoPergunta { get; private set; }
        public IPerguntaRespostaRepositorio PerguntaResposta { get; private set; }
        //NAO REMOVER ESSA LINHA 2
        #endregion

        public int SaveChanges()
        {
            var obj = new { };
            lock (obj)
            {
                return _contexto.SaveChanges();
            }
        }

        public void Rollback()
        {
            foreach (var entry in _contexto.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int ExecutarQuery(string query)
        {
            _contexto.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
            return _contexto.Database.ExecuteSqlRaw(query);
        }

        public List<T> ExecutarViewSQL<T>(string query)
        {
            using (var connection = new SqlConnection(_configuracao.GetConnectionString("Contexto")))
            {
                return connection.Query<T>(query).ToList();
            }
        }
    }
}
