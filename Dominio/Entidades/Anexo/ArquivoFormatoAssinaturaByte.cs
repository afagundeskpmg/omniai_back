namespace Dominio.Entidades
{
    public class ArquivoFormatoAssinaturaByte
    {
        public int Id { get; set; }
        public virtual ArquivoFormatoAssinatura ArquivoFormatoAssinatura { get; set; }
        public int ArquivoFormatoAssinaturaId { get; set; }
        public byte Byte { get; set; }
    }
}
