using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class DocumentoTipoConfiguracao : IEntityTypeConfiguration<DocumentoTipo>
    {
        public void Configure(EntityTypeBuilder<DocumentoTipo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(u => u.Id)
               .IsRequired()
               .HasMaxLength(36);

            builder.Property(p => p.Nome)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(uc => uc.Ambiente)
             .WithMany(u => u.DocumentosTipo)
             .HasForeignKey(uc => uc.AmbienteId)
             .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>
            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.DocumentosTipoCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.DocumentosTipoUltimaAlteracao)
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
