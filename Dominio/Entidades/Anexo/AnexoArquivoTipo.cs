namespace Dominio.Entidades
{
    public class AnexoArquivoTipo
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual int QuantidadeMaxima { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual ICollection<AnexoArquivoTipoArquivoFormato> FormatosPermitidos { get; set; }
    }
}
