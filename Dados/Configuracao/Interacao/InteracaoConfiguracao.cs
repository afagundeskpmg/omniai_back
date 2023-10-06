using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class InteracaoConfiguracao : IEntityTypeConfiguration<Interacao>
    {
        public void Configure(EntityTypeBuilder<Interacao> builder)
        {
            

            builder.HasKey(s => s.Id);

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(1500);

            builder.HasOne(i => i.InteracaoTipo)
                 .WithMany(it => it.Interacoes)
                 .HasForeignKey(i => i.InteracaoTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>

            builder.Property(p => p.CriadoEm)
                .IsRequired();

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.InteracoesCriadas)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.InteracoesUltimasAlteracoes)
                .HasForeignKey(p => p.UltimaAlteracaoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CriadoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.UltimaAlteracaoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.Excluido)
                .IsRequired();

            builder.ToTable("Interacao");

            //<FIM REGISTRO>
        }
    }
}
