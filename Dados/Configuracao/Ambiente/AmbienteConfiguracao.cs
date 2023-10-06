using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AmbienteConfiguracao : IEntityTypeConfiguration<Ambiente>
    {
        public void Configure(EntityTypeBuilder<Ambiente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.LimiteTokenPorRequisicao).IsRequired();

            builder.Property(x => x.CognitiveSearchIndexName)
                .HasMaxLength(50);

            builder.Property(x => x.CognitiveSearchIndexName)
                .HasMaxLength(50);

            builder.HasOne(x => x.Cliente)
                .WithMany(p => p.Ambientes)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Usuarios)
                .WithMany(a => a.Ambientes)
                .UsingEntity(
                    "UsuarioAmbiente",
                    l => l.HasOne(typeof(Usuario)).WithMany().HasForeignKey("UsuarioId").HasPrincipalKey("Id"),
                    r => r.HasOne(typeof(Ambiente)).WithMany().HasForeignKey("AmbienteId").HasPrincipalKey("Id"),
                    j => j.HasKey("UsuarioId", "AmbienteId"));

            //<REGISTRO>
            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.AmbientesCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.AmbientesUltimaAlteracao)
                .HasForeignKey(p => p.UltimaAlteracaoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CriadoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.UltimaAlteracaoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.Excluido)
                .IsRequired();
            //<FIM REGISTRO>
        }
    }
}
