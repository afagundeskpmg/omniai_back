using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class TemaConfiguracaoConfiguracao : IEntityTypeConfiguration<TemaConfiguracao>
    {
        public void Configure(EntityTypeBuilder<TemaConfiguracao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(u => u.UsuarioId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(x => x.LogoBg)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.NavbarBg)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.SidebarColor)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Layout)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.SidebarType)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Components)
               .IsRequired()
               .HasMaxLength(15);

            builder.Property(x => x.HeaderPosition)
                .IsRequired();

            builder.Property(x => x.BoxedLayout)
                .IsRequired();
        }
    }
}
