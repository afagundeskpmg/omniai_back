using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class AnexoPaginaConfiguracao : IEntityTypeConfiguration<AnexoPagina>
    {
        public void Configure(EntityTypeBuilder<AnexoPagina> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Ordem)
                .IsRequired();                          

            builder.HasOne(x => x.AnexoPai)
                .WithMany(at => at.Paginas)
                .HasForeignKey(x => x.AnexoPaiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AnexoPai)
                .WithMany(at => at.Paginas)
                .HasForeignKey(x => x.AnexoPaiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Anexo)
               .WithMany(at => at.PaginasPai)
               .HasForeignKey(x => x.AnexoId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
