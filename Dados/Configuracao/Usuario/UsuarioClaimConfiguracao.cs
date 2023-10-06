using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class UsuarioClaimConfiguracao : IEntityTypeConfiguration<UsuarioClaim>
    {
        public void Configure(EntityTypeBuilder<UsuarioClaim> builder)
        {
            builder.HasKey(uc => new { uc.UsuarioId, uc.ClaimType, uc.ClaimValue });

            builder.Property(uc => uc.ClaimType)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(uc => uc.ClaimValue)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(uc => uc.Usuario)
                .WithMany(u => u.Claims)
                .HasForeignKey(uc => uc.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(uc => uc.UsuarioId)
                .HasMaxLength(36);
        }
    }
}
