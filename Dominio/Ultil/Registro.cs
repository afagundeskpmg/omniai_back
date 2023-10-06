namespace Dominio.Entidades
{
    public class Registro
    {
        public virtual DateTime? CriadoEm { get; set; }
        public virtual string CriadoPorId { get; set; }
        public virtual Usuario CriadoPor { get; set; }
        public virtual DateTime? UltimaAlteracaoEm { get; set; }
        public virtual string UltimaAlteracaoPorId { get; set; }
        public virtual Usuario UltimaAlteracaoPor { get; set; }
        public virtual bool Excluido { get; set; }

        public void AtribuirInformacoesRegistroParaInsercao(string usuarioLogadoId)
        {
            CriadoEm = DateTime.Now;
            CriadoPorId = usuarioLogadoId;
            UltimaAlteracaoEm = DateTime.Now;
            UltimaAlteracaoPorId = usuarioLogadoId;
            
        }

        public void AtribuirInformacoesRegistroParaAlteracao(string usuarioLogadoId)
        {
            UltimaAlteracaoEm = DateTime.Now;
            UltimaAlteracaoPorId = usuarioLogadoId;
        }

    }
}
