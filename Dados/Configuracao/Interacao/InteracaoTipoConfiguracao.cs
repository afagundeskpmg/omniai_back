using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class InteracaoTipoConfiguracao : IEntityTypeConfiguration<InteracaoTipo>
    {
        public void Configure(EntityTypeBuilder<InteracaoTipo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}
