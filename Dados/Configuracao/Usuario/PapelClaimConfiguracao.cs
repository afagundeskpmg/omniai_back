using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PapelClaimConfiguracao : IEntityTypeConfiguration<PapelClaim>
    {
        public void Configure(EntityTypeBuilder<PapelClaim> builder)
        {
            builder.HasKey(x => new { x.PapelId, x.ClaimId });

            builder.Property(u => u.PapelId)
                .IsRequired()
                .HasMaxLength(36);

            builder.HasOne(x => x.Papel)
                .WithMany(p => p.Claims)
                .HasForeignKey(x => x.PapelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Claim)
                .WithMany(p => p.Papeis)
                .HasForeignKey(x => x.ClaimId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
