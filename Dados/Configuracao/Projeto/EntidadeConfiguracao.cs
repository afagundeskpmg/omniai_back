using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class EntidadeConfiguracao : IEntityTypeConfiguration<Entidade>
    {
        public void Configure(EntityTypeBuilder<Entidade> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(u => u.Id)
               .IsRequired()
               .HasMaxLength(36);

            builder.Property(p => p.Nome)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.Pergunta)
               .HasMaxLength(1000)
               .IsRequired();

            builder.Property(p => p.Query)
             .HasMaxLength(8000)
             .IsRequired();

            builder.Property(p => p.Dados)             
             .IsRequired()
             .HasColumnType("varchar(max)");

            builder.HasOne(uc => uc.DocumentoTipo)
             .WithMany(u => u.Entidades)
             .HasForeignKey(uc => uc.DocumentoTipoId)
             .OnDelete(DeleteBehavior.Restrict);

            //<REGISTRO>
            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.EntidadesCriadas)
                .HasForeignKey(p => p.CriadoPorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.EntidadesUltimaAlteracao)
                .HasForeignKey(p => p.UltimaAlteracaoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CriadoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.UltimaAlteracaoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.Excluido)
                .IsRequired();
            //<FIM REGISTRO>
        }
    }
}
