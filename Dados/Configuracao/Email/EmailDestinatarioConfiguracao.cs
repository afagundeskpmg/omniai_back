using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class EmailDestinatarioConfiguracao : IEntityTypeConfiguration<EmailDestinatario>
    {
        public void Configure(EntityTypeBuilder<EmailDestinatario> builder)
        {            

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Email)
                .WithMany(e => e.EmailsDestinatarios)
                .HasForeignKey(x => x.EmailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EmailDestinatarioTipo)
                .WithMany(e => e.EmailsDestinatarios)
                .HasForeignKey(x => x.EmailDestinatarioTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("EmailDestinatario");
        }
    }
}
