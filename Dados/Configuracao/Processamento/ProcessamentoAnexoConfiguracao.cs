using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoAnexoConfiguracao : IEntityTypeConfiguration<ProcessamentoAnexo>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoAnexo> builder)
        {

            builder.HasOne(x => x.ProjetoAnexo)
               .WithOne(a => a.ProcessamentoAnexo)
               .HasForeignKey<ProcessamentoAnexo>(ad => ad.ProjetoAnexoId);            

            builder.ToTable("ProcessamentoAnexo");
        }
    }
}
