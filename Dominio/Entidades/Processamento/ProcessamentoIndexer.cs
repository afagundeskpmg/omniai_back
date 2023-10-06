namespace Dominio.Entidades
{
    public class ProcessamentoIndexer : Processamento
    {        
        public ProcessamentoIndexer() { }
        public ProcessamentoIndexer(string usuarioId,ProcessamentoStatusEnum processamentoStatus,Projeto projeto) : base(usuarioId, (int)ProcessamentoTipoEnum.Indexer)
        {
            Projeto = projeto;
            ProjetoId = projeto.Id;
            Id = Guid.NewGuid().ToString();
            ProcessamentoStatusId = (int)processamentoStatus;            
            DataSourceName = Guid.NewGuid().ToString();
            IndexerName = Guid.NewGuid().ToString();                        
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
            Anexos = new List<ProjetoAnexo>();
        }

        public DateTime? InicioIndexacao { get; set; }
        public DateTime? FimIndexacao { get; set; }
        public DateTime? LiberadoEm { get; set; }
        public string? LiberadoPorId { get; set; }
        public string? ProjetoId { get; set; }        
        public string BlobFolder { get; set; }
        public string? Dados { get; set; }
        public string DataSourceName { get; set; }
        public string IndexerName { get; set; }
        public bool DeletarIndexer { get; set; }
        public virtual Usuario? LiberadoPor { get; set; }
        public virtual Projeto? Projeto { get; set; }        
        public virtual ICollection<ProjetoAnexo> Anexos { get; set; }
        public void Liberar(Usuario usuario)
        {
            LiberadoPor = usuario;
            LiberadoEm = DateTime.Now;
            AtribuirInformacoesRegistroParaAlteracao(usuario.Id);
        }
        public override object SerializarParaListar()
        {
            return new
            {
                Id,
                ProcessamentoStatus = ProcessamentoStatus.SerializarParaListar(),                
            };
        }
    }
}
