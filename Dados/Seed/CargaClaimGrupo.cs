using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaClaimGrupo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new ClaimGrupo() { Id = 1, Name = "Ambiente", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos ambientes da plataforma." },
                new ClaimGrupo() { Id = 2, Name = "Usuário", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos usuários da plataforma." },
                new ClaimGrupo() { Id = 3, Name = "Projeto", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos projetos da plataforma." },
                new ClaimGrupo() { Id = 4, Name = "Tipo de Documento", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos documentos da plataforma." },
                new ClaimGrupo() { Id = 5, Name = "Entidade", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos entidades da plataforma." },
                new ClaimGrupo() { Id = 6, Name = "Anexos", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos anexos da plataforma." },
                  new ClaimGrupo() { Id = 7, Name = "Processamentos", Descricao = "Estas alçadas concedem permissões relacionadas ao gerenciamento dos processamentos da plataforma." }
            };
        }
    }
}
