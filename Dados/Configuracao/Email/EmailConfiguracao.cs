using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class EmailConfiguracao : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Assunto)
                .HasMaxLength(100);

            builder.HasOne(x => x.CorpoAnexo)
               .WithOne(a => a.EmailCorpo)
               .HasForeignKey<Email>(ad => ad.CorpoAnexoId);

            builder.Property(x => x.RemetenteEmail)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EnviadoEm);

            builder.HasMany(x => x.Anexos)
                .WithMany(a => a.Emails)
                .UsingEntity(
                    "AnexoEmail",
                    l => l.HasOne(typeof(Anexo)).WithMany().HasForeignKey("AnexoId").HasPrincipalKey("Id").OnDelete(DeleteBehavior.Restrict),
                    r => r.HasOne(typeof(Email)).WithMany().HasForeignKey("EmailId").HasPrincipalKey("Id").OnDelete(DeleteBehavior.Restrict),
                    j => j.HasKey("AnexoId", "EmailId"));
        }
    }
}
