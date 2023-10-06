using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PaisConfiguracao : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CultureInfo)
                  .HasMaxLength(10);

            builder.Property(x => x.Flag)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DocumentoPadrao)
                .HasMaxLength(30);
        }
    }
}
