using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AnexoTipoConfiguracao : IEntityTypeConfiguration<AnexoTipo>
    {
        public void Configure(EntityTypeBuilder<AnexoTipo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
