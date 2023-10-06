using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PapelConfiguracao : IEntityTypeConfiguration<Papel>
    {
        public void Configure(EntityTypeBuilder<Papel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
