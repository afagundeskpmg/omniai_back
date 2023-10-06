namespace Dados.Interface
{
    public interface IUnitOfWorkRepositorio
    {
        #region Metodos
        int SaveChanges();
        void Dispose();
        int ExecutarQuery(string query);
        List<T> ExecutarViewSQL<T>(string query);
        #endregion

        #region Repositorios Interfaces    
        IBaseRepositorio<T> Repository<T>() where T : class;
        IPessoaJuridicaRepositorio PessoaJuridica { get; }
        IPessoaRepositorio Pessoa { get; }
        IAnexoRepositorio Anexo { get; }
        IAnexoArquivoTipoRepositorio AnexoArquivoTipo { get; }
        IArquivoFormatoRepositorio ArquivoFormato { get; }        
        IUsuarioRepositorio Usuario { get; }
        IAmbienteRepositorio Ambiente { get; }                
        IDocumentoTipoRepositorio DocumentoTipo { get; }
        IProjetoRepositorio Projeto { get; }
        IEntidadeRepositorio Entidade { get; }
        IProcessamentoStatusRepositorio ProcessamentoStatus { get; }
        IProjetoAnexoRepositorio ProjetoAnexo { get; }
        IProcessamentoIndexerRepositorio ProcessamentoIndexer { get; }        
        IProcessamentoRepositorio Processamento { get; }
        IProcessamentoTipoRepositorio ProcessamentoTipo { get; }
        IUsuarioClaimRepositorio UsuarioClaim { get; }
        IEmailRepositorio Email { get; }
        IProcessamentoAnexoRepositorio ProcessamentoAnexo { get; }
        IProcessamentoNerRepositorio ProcessamentoNer { get; }
        IProcessamentoPerguntaRepositorio ProcessamentoPergunta { get; }
        IPerguntaRespostaRepositorio PerguntaResposta { get; }
        //NAO REMOVER ESSA LINHA 1
        #endregion
    }
}
