using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PapelAcessivelConfiguracao : IEntityTypeConfiguration<PapelAcessivel>
    {
        public void Configure(EntityTypeBuilder<PapelAcessivel> builder)
        {
            builder.HasKey(x => new { x.PapelId, x.PapelAcessanteId });

            builder.HasOne(x => x.Papel)
                .WithMany(p => p.PapeisAcessantes)
                .HasForeignKey(x => x.PapelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.PapelId)
                 .IsRequired()
                 .HasMaxLength(36);

            builder.HasOne(x => x.PapelAcessante)
                .WithMany(p => p.PapeisAcessiveis)
                .HasForeignKey(x => x.PapelAcessanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.PapelAcessanteId)
                .IsRequired()
                .HasMaxLength(36);
        }
    }
}
