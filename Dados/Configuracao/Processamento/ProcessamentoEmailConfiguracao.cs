using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoEmailConfiguracao : IEntityTypeConfiguration<ProcessamentoEmail>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoEmail> builder)
        {
            builder.HasOne(x => x.Email)
                .WithOne(a => a.ProcessamentoEmail)
                .HasForeignKey<ProcessamentoEmail>(ad => ad.EmailId);

            builder.ToTable("ProcessamentoEmail");
        }
    }
}
