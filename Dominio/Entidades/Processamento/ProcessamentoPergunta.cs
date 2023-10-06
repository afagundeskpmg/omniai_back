namespace Dominio.Entidades
{
    public class ProcessamentoPergunta : Processamento
    {
        public ProcessamentoPergunta() { }
        public ProcessamentoPergunta(string usuarioId, ProcessamentoStatusEnum processamentoStatus, ProjetoAnexo projetoAnexo, Entidade entidade) : base(usuarioId, (int)ProcessamentoTipoEnum.Pergunta)
        {
            Id = Guid.NewGuid().ToString();
            ProcessamentoStatusId = (int)processamentoStatus;
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
            ProjetoAnexo = projetoAnexo;
            Entidade = entidade;
            PerguntaResposta = new PerguntaResposta(projetoAnexo.Projeto, entidade.Query, CaminhoBlobStorage);
            PerguntaRespostaId = PerguntaResposta.Id;
        }
        public int ProjetoAnexoId { get; set; }
        public string ProcessamentoNerId { get; set; }
        public string? PerguntaRespostaId { get; set; }
        public string EntidadeId { get; set; }        
        public DateTime? InicioConsulta { get; set; }
        public DateTime? FimConsulta { get; set; }
        public virtual PerguntaResposta? PerguntaResposta { get; set; }
        public virtual ProjetoAnexo ProjetoAnexo { get; set; }
        public virtual Entidade Entidade { get; set; }
        public virtual ProcessamentoNer ProcessamentoNer { get; set; }
        public string CaminhoBlobStorage
        {
            get
            {
                return @Path.Combine("/Ambiente", ProjetoAnexo.Projeto.AmbienteId.ToString(), "Anexos", "Projetos", ProjetoAnexo.ProjetoId, "Processamentos", ProjetoAnexo.ProcessamentoIndexerId.ToString(), "DocumentosPaginas", ProjetoAnexo.AnexoId.ToString()).Replace("\\", "/");
            }
        }
        public override object SerializarParaListar()
        {
            return new
            {
                Id,
                ProcessamentoStatus = ProcessamentoStatus.SerializarParaListar(),
                File = ProjetoAnexo.Anexo.Serializar(),
                Entidade = Entidade.SerializarParaListar(),

            };
        }
    }
}
