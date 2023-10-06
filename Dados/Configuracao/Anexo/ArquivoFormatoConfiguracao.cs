using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ArquivoFormatoConfiguracao : IEntityTypeConfiguration<ArquivoFormato>
    {
        public void Configure(EntityTypeBuilder<ArquivoFormato> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.MimeType)
                .IsRequired()
                .HasMaxLength(75);
        }
    }
}
