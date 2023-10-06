namespace Dominio.Entidades
{
    public class ArquivoFormato
    {
        public ArquivoFormato()
        {
            ArquivoFormatoAssinaturas = new List<ArquivoFormatoAssinatura>();
        }

        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string MimeType { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual ICollection<AnexoArquivoTipoArquivoFormato> AnexoArquivoTipos { get; set; }
        public virtual double TamanhoMaximoMb { get; set; }
        public virtual List<ArquivoFormatoAssinatura> ArquivoFormatoAssinaturas { get; set; }

        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Nome
            };
        }
    }
}
