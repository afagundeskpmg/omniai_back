using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ArquivoFormatoAssinaturaConfiguracao : IEntityTypeConfiguration<ArquivoFormatoAssinatura>
    {
        public void Configure(EntityTypeBuilder<ArquivoFormatoAssinatura> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ArquivoFormato)
                .WithMany(at => at.ArquivoFormatoAssinaturas)
                .HasForeignKey(x => x.ArquivoFormatoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
