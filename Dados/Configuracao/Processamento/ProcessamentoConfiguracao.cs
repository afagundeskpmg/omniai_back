using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoConfiguracao : IEntityTypeConfiguration<Processamento>
    {
        public void Configure(EntityTypeBuilder<Processamento> builder)
        {           

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);            

            builder.Property(x => x.QueueExpiraEm);

            builder.Property(x => x.QueueMessageId)
                .HasMaxLength(36);

            builder.Property(x => x.FimProcessamentoEm);

            builder.Property(x => x.InicioProcessamentoEm);

            builder.HasOne(x => x.ProcessamentoStatus)
                .WithMany(s => s.Processamentos)
                .HasForeignKey(x => x.ProcessamentoStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProcessamentoTipo)
                .WithMany(s => s.Processamentos)
                .HasForeignKey(x => x.ProcessamentoTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>

            builder.Property(p => p.CriadoEm)
                .IsRequired();

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.ProcessamentosCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.ProcessamentosUltimasAlteracoes)
                .HasForeignKey(p => p.UltimaAlteracaoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CriadoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.UltimaAlteracaoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.Excluido)
                .IsRequired();

            builder.ToTable("Processamento");

            //<FIM REGISTRO>
        }
    }
}
