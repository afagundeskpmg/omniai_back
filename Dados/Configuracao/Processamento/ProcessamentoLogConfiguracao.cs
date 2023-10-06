using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoLogConfiguracao : IEntityTypeConfiguration<ProcessamentoLog>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(8000);

            builder.Property(x => x.CriadoEm)
                .IsRequired();

            builder.HasOne(x => x.Processamento)
                 .WithMany(s => s.ProcessamentoLogs)
                 .HasForeignKey(x => x.ProcessamentoId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
