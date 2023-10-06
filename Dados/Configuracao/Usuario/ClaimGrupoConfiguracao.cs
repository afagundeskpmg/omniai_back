using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ClaimGrupoConfiguracao : IEntityTypeConfiguration<ClaimGrupo>
    {
        public void Configure(EntityTypeBuilder<ClaimGrupo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
