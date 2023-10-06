namespace Dominio.Entidades
{
    public class AnexoArquivoTipoArquivoFormato
    {
        public int AnexoArquivoTipoId { get; set; }
        public virtual AnexoArquivoTipo AnexoArquivoTipo { get; set; }
        public int ArquivoFormatoId { get; set; }
        public virtual ArquivoFormato ArquivoFormato { get; set; }
    }
}
