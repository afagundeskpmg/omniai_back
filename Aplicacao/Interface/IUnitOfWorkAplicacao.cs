namespace Aplicacao.Interface
{
    public interface IUnitOfWorkAplicacao : IDisposable
    {
        int SaveChanges();
        void ConfigurarEntidadesNoUnitOfWork(params string[] nomesDasEntidades);

        #region Interfaces    

        IAnexoAplicacao Anexo { get; }
        IPessoaJuridicaAplicacao PessoaJuridica { get; }
        IPessoaAplicacao Pessoa { get; }
        IArquivoFormatoAplicacao ArquivoFormato { get; }        
        IUsuarioAplicacao Usuario { get; }
        IAmbienteAplicacao Ambiente { get; }
        IAnexoArquivoTipoAplicacao AnexoArquivoTipo { get; }                
        IDocumentoTipoAplicacao DocumentoTipo { get; }
        IProjetoAplicacao Projeto { get; }
        IEntidadeAplicacao Entidade { get; }
        IProcessamentoStatusAplicacao ProcessamentoStatus { get; }
        IProjetoAnexoAplicacao ProjetoAnexo { get; }
        IProcessamentoIndexerAplicacao ProcessamentoIndexer { get; }        
        IProcessamentoAplicacao Processamento { get; }
        IProcessamentoTipoAplicacao ProcessamentoTipo { get; }
        IUsuarioClaimAplicacao UsuarioClaim { get; }
        IEmailAplicacao Email { get; }
        IProcessamentoAnexoAplicacao ProcessamentoAnexo { get; }
        IProcessamentoNerAplicacao ProcessamentoNer { get; }
        IProcessamentoPerguntaAplicacao ProcessamentoPergunta { get; }
        IPerguntaRespostaAplicacao PerguntaResposta { get; }
        //NAO REMOVER ESSA LINHA 1
        #endregion
    }
}
