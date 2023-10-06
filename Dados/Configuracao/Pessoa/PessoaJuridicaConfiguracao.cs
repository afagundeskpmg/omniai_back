using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PessoaJuridicaConfiguracao : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.Property(p => p.DadosCadastrais)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.ToTable("PessoaJuridica");
        }
    }
}
