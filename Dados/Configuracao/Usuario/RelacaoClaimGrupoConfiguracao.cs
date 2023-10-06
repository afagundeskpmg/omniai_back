using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class RelacaoClaimGrupoConfiguracao : IEntityTypeConfiguration<RelacaoClaimGrupo>
    {
        public void Configure(EntityTypeBuilder<RelacaoClaimGrupo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(r => r.Descricao)
                .IsRequired()
                .HasMaxLength(750);

            builder.HasOne(r => r.Grupo)
                .WithMany(g => g.ClaimsImpactantes)
                .HasForeignKey(r => r.ClaimGrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Claim)
                .WithMany(c => c.GruposImpactados)
                .HasForeignKey(r => r.ClaimId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
