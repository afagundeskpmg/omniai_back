using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ClaimConfiguracao : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.Property(c => c.ClaimType)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DefaultValue)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(c => c.ClaimPai)
              .WithMany(g => g.ClaimsFilhas)
              .HasForeignKey(c => c.ClaimPaiId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
