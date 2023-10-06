using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ArquivoFormatoAssinaturaByteConfiguracao : IEntityTypeConfiguration<ArquivoFormatoAssinaturaByte>
    {
        public void Configure(EntityTypeBuilder<ArquivoFormatoAssinaturaByte> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasOne(x => x.ArquivoFormatoAssinatura)
                .WithMany(at => at.ArquivoFormatoAssinaturaBytes)
                .HasForeignKey(x => x.ArquivoFormatoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
