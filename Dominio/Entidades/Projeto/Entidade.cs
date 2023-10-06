using Dominio.Validacoes;

namespace Dominio.Entidades
{
    public class Entidade : Registro
    {
        public Entidade() { }
        public Entidade(string nome,string pergunta,string documentoTipoId,string usuarioId) 
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Pergunta = pergunta;
            DocumentoTipoId = documentoTipoId;
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Pergunta { get; set; }
        public string Query { get; set; }
        public string Dados { get; set; }        
        public string DocumentoTipoId { get; set; }
        public virtual DocumentoTipo DocumentoTipo { get; set; }
        public virtual ICollection<ProcessamentoPergunta> ProcessamentosPerguntas { get; set; }
        public object Serializar() {

            return new
            {
                Id,
                Nome,              
                Pergunta,
                Query,
                DocumentoTipo = DocumentoTipo.Serializar(),
            };
        }
        public object SerializarParaListar() 
        {
            return new
            {
                Id,
                Nome,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorNome = CriadoPor.Nome,
                Pergunta,
                Query,
                DocumentoTipo = DocumentoTipo.Serializar(),
            };
        }

    }
}