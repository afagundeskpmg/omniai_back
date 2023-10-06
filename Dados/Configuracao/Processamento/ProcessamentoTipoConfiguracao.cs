using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoTipoConfiguracao : IEntityTypeConfiguration<ProcessamentoTipo>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoTipo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(i => i.QueueNome)                  
                  .HasMaxLength(50);

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
