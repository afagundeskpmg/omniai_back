namespace Dominio.Entidades
{
    public class ArquivoFormatoAssinatura
    {
        public ArquivoFormatoAssinatura()
        {
            ArquivoFormatoAssinaturaBytes = new List<ArquivoFormatoAssinaturaByte>();
        }

        public int Id { get; set; }
        public virtual ArquivoFormato ArquivoFormato { get; set; }
        public int ArquivoFormatoId { get; set; }
        public virtual List<ArquivoFormatoAssinaturaByte> ArquivoFormatoAssinaturaBytes { get; set; }
    }
}
