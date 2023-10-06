using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaProcessamentoTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new ProcessamentoTipo() { Id = (int)ProcessamentoTipoEnum.Indexer, Nome = "Indexer" ,QueueNome="processamentoindexer"},
                new ProcessamentoTipo() { Id = (int)ProcessamentoTipoEnum.Email, Nome = "Email",QueueNome="processamentoemail" },
                new ProcessamentoTipo() { Id = (int)ProcessamentoTipoEnum.Anexo, Nome = "Anexo" ,QueueNome="processamentoanexo"},
                new ProcessamentoTipo() { Id = (int)ProcessamentoTipoEnum.Ner, Nome = "Ner" ,QueueNome="processamentoner"},
                new ProcessamentoTipo() { Id = (int)ProcessamentoTipoEnum.Pergunta, Nome = "Pergunta" ,QueueNome="processamentopergunta"},
            };
        }
    }
}
