using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PessoaTipoConfiguracao : IEntityTypeConfiguration<PessoaTipo>
    {
        public void Configure(EntityTypeBuilder<PessoaTipo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
