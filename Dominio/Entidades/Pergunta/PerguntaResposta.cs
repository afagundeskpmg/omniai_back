namespace Dominio.Entidades
{
    public class PerguntaResposta
    {
        public PerguntaResposta(Projeto projeto, string pergunta, string caminhoBlob)
        {
            Id = Guid.NewGuid().ToString();
            PaginasRelacionadas = new List<AnexoPagina>();
            Projeto  = projeto;
            ProjetoId = projeto.Id;
            Pergunta = pergunta;
            CaminhoBlob = caminhoBlob;
        }
        public PerguntaResposta() 
        {
            Id = Guid.NewGuid().ToString();
            PaginasRelacionadas = new List<AnexoPagina>();
        }
        public string Id { get; set; }
        public string? ProjetoId { get; set; }
        public string? Prompt { get; set; }
        public string? Pergunta { get; set; }
        public string? Resposta { get; set; }
        public int? TokensPergunta { get; set; }
        public int? TokensResposta { get; set; }
        public string? Dados { get; set; }
        public string CaminhoBlob { get; set; }
        public virtual Projeto? Projeto { get; set; }
        public virtual ICollection<AnexoPagina> PaginasRelacionadas { get; set; }
        public virtual ICollection<ProcessamentoPergunta> ProcessamentoPerguntas { get; set; }
        public object Serializar() 
        {
            var paginasRelacionadas = new List<object>();

            if (PaginasRelacionadas != null && PaginasRelacionadas.Any())
                PaginasRelacionadas.ToList().ForEach(x => paginasRelacionadas.Add(x.Serializar()));

            return new
            {
                Resposta,
                PaginasRelacionas = paginasRelacionadas
            };
        }
    }
}
