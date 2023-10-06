using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class DocumentoConfiguracao : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Mascara)
                .HasMaxLength(50);

            builder.Property(x => x.Principal)
                .IsRequired();

            builder.HasOne(x => x.PessoaTipo)
                .WithMany(p => p.DocumentoTipos)
                .HasForeignKey(x => x.PessoaTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Pais)
                .WithMany(p => p.DocumentoTipos)
                .HasForeignKey(x => x.PaisId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
