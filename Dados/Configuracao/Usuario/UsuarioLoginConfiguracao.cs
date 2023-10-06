using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class UsuarioLoginConfiguracao : IEntityTypeConfiguration<UsuarioLogin>
    {
        public void Configure(EntityTypeBuilder<UsuarioLogin> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LoginProvider)
                .HasMaxLength(100);

            builder.Property(l => l.ProviderKey)
                .HasMaxLength(100);

            builder.Property(l => l.UserId)
               .HasMaxLength(36);

            builder.HasOne(l => l.User)
                .WithMany(u => u.Logins)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
