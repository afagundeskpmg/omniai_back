using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AnexoArquivoTipoArquivoFormatoConfiguracao : IEntityTypeConfiguration<AnexoArquivoTipoArquivoFormato>
    {
        public void Configure(EntityTypeBuilder<AnexoArquivoTipoArquivoFormato> builder)
        {
            builder.HasKey(x => new { x.AnexoArquivoTipoId, x.ArquivoFormatoId });

            builder.HasOne(a => a.ArquivoFormato)
                .WithMany(f => f.AnexoArquivoTipos)
                .HasForeignKey(a => a.ArquivoFormatoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.AnexoArquivoTipo)
                .WithMany(f => f.FormatosPermitidos)
                .HasForeignKey(a => a.AnexoArquivoTipoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
