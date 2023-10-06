namespace Dominio.Entidades
{
    public class ProcessamentoNer : Processamento
    {
        public ProcessamentoNer() { }
        public ProcessamentoNer(string usuarioId, ProcessamentoStatusEnum processamentoStatus) : base(usuarioId, (int)ProcessamentoTipoEnum.Ner)
        {
            Id = Guid.NewGuid().ToString();
            ProcessamentoStatusId = (int)processamentoStatus;
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
        }
        public string ProjetoId { get; set; }
        public virtual Projeto? Projeto { get; set; }
        public virtual ICollection<ProcessamentoPergunta> ProcessamentosPerguntaGeradas { get; set; }
        public override object SerializarParaListar()
        {
            if (ProcessamentosPerguntaGeradas != null && ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComSucesso && ProcessamentosPerguntaGeradas.Any(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComSucesso))
            {
                var dados = ProcessamentosPerguntaGeradas
                            .GroupBy(pp => pp.ProjetoAnexo.Anexo.NomeArquivoOriginal)
                            .Select(grupo => new
                            {
                                Arquivo = new
                                {
                                    Nome = grupo.Key
                                },
                                Entidades = grupo.Select(pp => new
                                {
                                    Nome = pp.Entidade.Nome,
                                    Resposta = pp.PerguntaResposta.Resposta,
                                    PaginasRelacionadas = pp.PerguntaResposta.PaginasRelacionadas.Select(x => x.Serializar()),
                                    TokensPergunta = pp.PerguntaResposta.TokensPergunta,
                                    TokensResposta = pp.PerguntaResposta.TokensResposta,

                                }).ToList()
                            }).ToList();

                return new
                {
                    Id,
                    Projeto = Projeto.Serializar(),
                    ProcessamentoStatus = ProcessamentoStatus.SerializarParaListar(),
                    Dados = dados

                };
            }
            else 
            {
                return new
                {
                    Id,
                    Projeto = Projeto.Serializar(),
                    ProcessamentoStatus = ProcessamentoStatus.SerializarParaListar(),                    

                };
            }

            
        }
    }
}
