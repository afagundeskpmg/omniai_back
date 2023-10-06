namespace Dominio.Entidades
{
    public abstract class Interacao : Registro
    {
        public Interacao()
        {
        }

        public Interacao(string usuarioId, string descricao, int tipoId)
        {
            Descricao = descricao.Trim();
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
            InteracaoTipoId = tipoId;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual InteracaoTipo InteracaoTipo { get; set; }
        public int InteracaoTipoId { get; set; }

        public abstract object SerializarParaListar();
    }
}
