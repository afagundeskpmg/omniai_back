using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoStatusConfiguracao : IEntityTypeConfiguration<ProcessamentoStatus>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoStatus> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
