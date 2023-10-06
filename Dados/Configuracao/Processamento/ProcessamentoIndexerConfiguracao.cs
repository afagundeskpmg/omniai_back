using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoIndexerConfiguracao : IEntityTypeConfiguration<ProcessamentoIndexer>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoIndexer> builder)
        {
            builder.HasOne(uc => uc.Projeto)
             .WithMany(u => u.ProcessamentosIndexers)
             .HasForeignKey(uc => uc.ProjetoId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.LiberadoPor)
            .WithMany(u => u.ProcessamentosIndexacaoLiberados)
            .HasForeignKey(uc => uc.LiberadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IndexerName)
             .HasMaxLength(36);

            builder.Property(x => x.BlobFolder)
            .HasMaxLength(8000);

            builder.Property(x => x.Dados)
             .HasColumnType("varchar(max)");

            builder.ToTable("ProcessamentoIndexer");
        }
    }
}
