using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PessoaConfiguracao : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            //

            builder.HasKey(x => x.Id);

            builder.HasIndex(p => p.Documento);

            builder.Property(p => p.Nome)
                .HasMaxLength(800)
                .IsRequired();

            builder.Property(p => p.Documento)
                .HasMaxLength(20);

            builder.HasOne(p => p.DocumentoTipo)
                .WithMany(dt => dt.Pessoas)
                .HasForeignKey(p => p.DocumentoTipoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PessoaTipo)
                .WithMany(pc => pc.Pessoas)
                .HasForeignKey(p => p.PessoaTipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Pais)
                .WithMany(pc => pc.Pessoas)
                .HasForeignKey(p => p.PaisId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Ativo)
                .IsRequired();

            //<REGISTRO>
            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.PessoasCriadas)
                .HasForeignKey(p => p.CriadoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.PessoasUltimaAlteracao)
                .HasForeignKey(p => p.UltimaAlteracaoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CriadoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.UltimaAlteracaoPorId)
                .HasMaxLength(36);

            builder.Property(p => p.Excluido)
                .IsRequired();

            builder.ToTable("Pessoa");
            //<FIM REGISTRO>
        }
    }
}
