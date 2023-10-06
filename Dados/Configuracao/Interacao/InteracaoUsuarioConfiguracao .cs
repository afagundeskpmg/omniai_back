using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class InteracaoUsuarioConfiguracao : IEntityTypeConfiguration<InteracaoUsuario>
    {
        public void Configure(EntityTypeBuilder<InteracaoUsuario> builder)
        {
            builder.HasOne(i => i.Usuario)
                 .WithMany(it => it.Interacoes)
                 .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("InteracaoUsuario");

        }
    }
}
