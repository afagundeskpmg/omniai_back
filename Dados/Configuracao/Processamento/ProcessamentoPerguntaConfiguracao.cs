using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dados.Configuracao
{
    public class ProcessamentoPerguntaConfiguracao : IEntityTypeConfiguration<ProcessamentoPergunta>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoPergunta> builder)
        {
            builder.HasOne(uc => uc.ProjetoAnexo)
             .WithMany(u => u.ProcessamentosPerguntas)
             .HasForeignKey(uc => uc.ProjetoAnexoId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.Entidade)
            .WithMany(u => u.ProcessamentosPerguntas)
            .HasForeignKey(uc => uc.EntidadeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.ProcessamentoNer)
            .WithMany(u => u.ProcessamentosPerguntaGeradas)
            .HasForeignKey(uc => uc.ProcessamentoNerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.PerguntaResposta)
            .WithMany(u => u.ProcessamentoPerguntas)
            .HasForeignKey(uc => uc.PerguntaRespostaId)
            .OnDelete(DeleteBehavior.Restrict);            

            builder.ToTable("ProcessamentoPergunta");
        }
    }
}
