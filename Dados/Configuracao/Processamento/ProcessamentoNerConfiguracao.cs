using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoNerConfiguracao : IEntityTypeConfiguration<ProcessamentoNer>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoNer> builder)
        {
            builder.HasOne(uc => uc.Projeto)
             .WithMany(u => u.ProcessamentosNer)
             .HasForeignKey(uc => uc.ProjetoId)
             .OnDelete(DeleteBehavior.Restrict);            

            builder.ToTable("ProcessamentoNer");
        }
    }
}
