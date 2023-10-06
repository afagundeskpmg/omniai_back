using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class PerguntaRespostaConfiguracao : IEntityTypeConfiguration<PerguntaResposta>
    {
        public void Configure(EntityTypeBuilder<PerguntaResposta> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.HasOne(uc => uc.Projeto)
             .WithMany(u => u.PerguntasRespostas)
             .HasForeignKey(uc => uc.ProjetoId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Pergunta)
             .HasColumnType("varchar(max)");

            builder.Property(x => x.Prompt)
            .HasColumnType("varchar(max)");

            builder.Property(x => x.Resposta)
             .HasColumnType("varchar(max)");

            builder.Property(p => p.Dados)            
            .HasColumnType("varchar(max)");

            builder.ToTable("PerguntaResposta");
        }
    }
}
