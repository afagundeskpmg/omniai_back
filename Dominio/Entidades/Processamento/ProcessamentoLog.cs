namespace Dominio.Entidades
{
    public class ProcessamentoLog
    {
        public ProcessamentoLog(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao))
            {
                Descricao = descricao;

                if (Descricao.Length > 8000)
                    Descricao = descricao.Substring(0, 7900);
            }

            CriadoEm = DateTime.Now;
        }

        public ProcessamentoLog()
        {
            CriadoEm = DateTime.Now;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public string ProcessamentoId { get; set; }
        public virtual Processamento Processamento { get; set; }

        public object SerializarParaListarFiltro()
        {
            return new { Descricao, CriadoEm = CriadoEm == null ? "" : CriadoEm.ToString() };
        }
    }
}
