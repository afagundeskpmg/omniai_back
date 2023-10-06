using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AnexoConfiguracao : IEntityTypeConfiguration<Anexo>
    {
        public void Configure(EntityTypeBuilder<Anexo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeArquivoAlterado)
                .IsRequired()
                .HasMaxLength(68);

            builder.Property(x => x.NomeArquivoOriginal)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CaminhoArquivoBlobStorage)
                .HasMaxLength(300);

            builder.Property(x => x.BlobContainerName)                 
                 .HasMaxLength(100);

            builder.HasOne(x => x.AnexoTipo)
                .WithMany(at => at.Anexos)
                .HasForeignKey(x => x.AnexoTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ArquivoFormato)
                .WithMany(at => at.Anexos)
                .HasForeignKey(x => x.ArquivoFormatoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AnexoArquivoTipo)
                .WithMany(at => at.Anexos)
                .HasForeignKey(x => x.AnexoArquivoTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>

            builder.Property(p => p.CriadoEm)
                .IsRequired();

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.AnexosCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.AnexosUltimaAlteracao)
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
