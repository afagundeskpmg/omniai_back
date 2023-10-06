using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class EmailDestinatarioUsuarioConfiguracao : IEntityTypeConfiguration<EmailDestinatarioUsuario>
    {
        public void Configure(EntityTypeBuilder<EmailDestinatarioUsuario> builder)
        {
            builder.HasOne(x => x.Usuario)
                .WithMany(e => e.Emails)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UsuarioId)
                .HasMaxLength(36);

            builder.ToTable("EmailDestinatarioUsuario");
        }
    }
}
