using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProjetoConfiguracao : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(p => p.Nome)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(uc => uc.Ambiente)
              .WithMany(u => u.Projetos)
              .HasForeignKey(uc => uc.AmbienteId)
              .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>
            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.ProjetosCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.ProjetosUltimaAlteracao)
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
