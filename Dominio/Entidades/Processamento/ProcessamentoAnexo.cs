namespace Dominio.Entidades
{
    public class ProcessamentoAnexo : Processamento
    {        
        public ProcessamentoAnexo() { }
        public ProcessamentoAnexo(string usuarioId,ProcessamentoStatusEnum processamentoStatus,ProjetoAnexo projetoanexo) : base(usuarioId, (int)ProcessamentoTipoEnum.Anexo)
        {
            Id = Guid.NewGuid().ToString();
            ProcessamentoStatusId =(int)processamentoStatus;                                         
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
            ProjetoAnexo = projetoanexo;
        }        
        public int ProjetoAnexoId { get; set; }
        public virtual ProjetoAnexo ProjetoAnexo { get; set; }                

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
