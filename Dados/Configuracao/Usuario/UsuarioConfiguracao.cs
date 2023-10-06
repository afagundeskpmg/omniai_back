using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Nome)
                .HasMaxLength(150);

            builder.HasOne(u => u.Papel)
                 .WithMany(p => p.Usuarios)
                 .HasForeignKey(u => u.PapelId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.UltimoAcessoIP);                

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasIndex(u => u.UserName)
                .IsUnique();            

            builder.HasOne(u => u.TemaConfiguracao)
                 .WithOne(p => p.Usuario)
                 .HasForeignKey<TemaConfiguracao>(et => et.UsuarioId);
       
            builder.Property(u => u.UltimoAcessoEm);

            //<REGISTRO>

            builder.Property(p => p.CriadoEm);

            builder.HasOne(p => p.CriadoPor)
                .WithMany(u => u.UsuariosCriados)
                .HasForeignKey(p => p.CriadoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UltimaAlteracaoEm);

            builder.HasOne(p => p.UltimaAlteracaoPor)
                .WithMany(u => u.UsuariosUltimaAlteracao)
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
