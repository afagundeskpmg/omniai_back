namespace Dominio.Entidades
{
    public abstract class Processamento : Registro
    {
        public Processamento()
        {
            ProcessamentoStatusId = (int)ProcessamentoStatusEnum.ProcessamentoSolicitado;
        }

        public Processamento(string usuarioId, int tipoId)
        {
            Id = Guid.NewGuid().ToString();
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
            ProcessamentoTipoId = tipoId;
            ProcessamentoStatusId = (int)ProcessamentoStatusEnum.ProcessamentoSolicitado;
        }

        public string Id { get; set; }
        public string? QueueMessageId { get; set; }
        public DateTime? QueueExpiraEm { get; set; }
        public DateTime? FimProcessamentoEm { get; set; }
        public DateTime? InicioProcessamentoEm { get; set; }
        public int ProcessamentoStatusId { get; set; }
        public virtual ProcessamentoStatus ProcessamentoStatus { get; set; }
        public int ProcessamentoTipoId { get; set; }
        public virtual ProcessamentoTipo ProcessamentoTipo { get; set; }
        public virtual ICollection<ProcessamentoLog> ProcessamentoLogs { get; set; }

        public abstract object SerializarParaListar();
        public void FinalizarProcessamentoComStatus(ProcessamentoStatusEnum processamentoStatusEnum, string usuarioId, ProcessamentoLog log)
        {
            usuarioId = usuarioId == null ? CriadoPorId : usuarioId;
            ProcessamentoStatusId = (int)processamentoStatusEnum;
            FimProcessamentoEm = DateTime.Now;
            AtribuirInformacoesRegistroParaAlteracao(usuarioId);

            if (log != null)
                ProcessamentoLogs.Add(log);
        }
        public void AtualizarStatusDeProcessamento(ProcessamentoStatusEnum processamentoStatusEnum, string usuarioId)
        {         
            ProcessamentoStatusId = (int)processamentoStatusEnum;
            InicioProcessamentoEm = DateTime.Now;

            if (!string.IsNullOrEmpty(usuarioId))
                AtribuirInformacoesRegistroParaAlteracao(usuarioId);
        }
        public void AtribuirLog(ProcessamentoLog log)
        {
            if (ProcessamentoLogs == null)
                ProcessamentoLogs = new List<ProcessamentoLog>();

            if (log != null)
                ProcessamentoLogs.Add(log);
        }
    }
}
