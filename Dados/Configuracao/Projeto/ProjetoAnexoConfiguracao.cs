using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProjetoAnexoConfiguracao : IEntityTypeConfiguration<ProjetoAnexo>
    {
        public void Configure(EntityTypeBuilder<ProjetoAnexo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(uc => uc.Anexo)
                .WithMany(u => u.Projetos)
                .HasForeignKey(uc => uc.AnexoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.ProcessamentoIndexer)
             .WithMany(u => u.Anexos)
             .HasForeignKey(uc => uc.ProcessamentoIndexerId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProcessamentoAnexo)
               .WithOne(a => a.ProjetoAnexo)
               .HasForeignKey<ProcessamentoAnexo>(ad => ad.Id);

            builder.HasOne(uc => uc.DocumentoTipo)
              .WithMany(u => u.Anexos)
              .HasForeignKey(uc => uc.DocumentoTipoId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.Projeto)
             .WithMany(u => u.Anexos)
             .HasForeignKey(uc => uc.ProjetoId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
