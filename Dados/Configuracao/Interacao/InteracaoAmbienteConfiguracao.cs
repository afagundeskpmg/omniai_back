using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class InteracaoAmbienteConfiguracao : IEntityTypeConfiguration<InteracaoAmbiente>
    {
        public void Configure(EntityTypeBuilder<InteracaoAmbiente> builder)
        {
            builder.HasOne(i => i.Ambiente)
                 .WithMany(it => it.Interacoes)
                 .HasForeignKey(i => i.AmbienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("InteracaoAmbiente");

        }
    }
}
