using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AnexoArquivoTipoConfiguracao : IEntityTypeConfiguration<AnexoArquivoTipo>
    {
        public void Configure(EntityTypeBuilder<AnexoArquivoTipo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.QuantidadeMaxima)
                .IsRequired();
        }
    }
}
