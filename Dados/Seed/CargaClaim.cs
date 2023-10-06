using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaClaim
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new Claim() { Id = 1001, ClaimPaiId = 1002, ClaimType = "AlterarTodosAmbientes", DefaultValue = "True", Descricao = "Alterar todos os ambientes" },
                new Claim() { Id = 1002, ClaimPaiId = null, ClaimType = "VisualizarTodosAmbientes", DefaultValue = "True", Descricao = "Visualizar todos os ambientes" },
                new Claim() { Id = 1003, ClaimPaiId = 1004, ClaimType = "AlterarMeusAmbientes", DefaultValue = "True", Descricao = "Alterar meus ambientes" },
                new Claim() { Id = 1004, ClaimPaiId = null, ClaimType = "VisualizarMeusAmbientes", DefaultValue = "True", Descricao = "Visualizar meus ambientes" },

                new Claim() { Id = 2001, ClaimPaiId = 2002, ClaimType = "AlterarTodosUsuarios", DefaultValue = "True", Descricao = "Alterar todos os usuários" },
                new Claim() { Id = 2002, ClaimPaiId = null, ClaimType = "VisualizarTodosUsuarios", DefaultValue = "True", Descricao = "Visualizar todos os usuários" },
                new Claim() { Id = 2003, ClaimPaiId = 2004, ClaimType = "AlterarMeusUsuarios", DefaultValue = "True", Descricao = "Alterar meus usuários" },
                new Claim() { Id = 2004, ClaimPaiId = null, ClaimType = "VisualizarMeusUsuarios", DefaultValue = "True", Descricao = "Visualizar meus usuários" },

                new Claim() { Id = 3001, ClaimPaiId = 3002, ClaimType = "AlterarTodosProjetos", DefaultValue = "True", Descricao = "Alterar todos os projetos" },
                new Claim() { Id = 3002, ClaimPaiId = null, ClaimType = "VisualizarTodosProjetos", DefaultValue = "True", Descricao = "Visualizar todos os projetos" },
                new Claim() { Id = 3003, ClaimPaiId = 3004, ClaimType = "AlterarMeusProjetos", DefaultValue = "True", Descricao = "Alterar meus projetos" },
                new Claim() { Id = 3004, ClaimPaiId = null, ClaimType = "VisualizarMeusProjetos", DefaultValue = "True", Descricao = "Visualizar meus projetos" },

                new Claim() { Id = 4001, ClaimPaiId = 4002, ClaimType = "AlterarTodosDocumentosTipo", DefaultValue = "True", Descricao = "Alterar todos os tipos de documentos" },
                new Claim() { Id = 4002, ClaimPaiId = null, ClaimType = "VisualizarTodosDocumentosTipo", DefaultValue = "True", Descricao = "Visualizar todos os tipos de documentos" },
                new Claim() { Id = 4003, ClaimPaiId = 4004, ClaimType = "AlterarMeusDocumentosTipo", DefaultValue = "True", Descricao = "Alterar meus tipos de documentos" },
                new Claim() { Id = 4004, ClaimPaiId = null, ClaimType = "VisualizarMeusDocumentosTipo", DefaultValue = "True", Descricao = "Visualizar meus tipos de documentos" },

                new Claim() { Id = 5001, ClaimPaiId = 5002, ClaimType = "AlterarTodosEntidades", DefaultValue = "True", Descricao = "Alterar todas as entidades" },
                new Claim() { Id = 5002, ClaimPaiId = null, ClaimType = "VisualizarTodasEntidades", DefaultValue = "True", Descricao = "Visualizar todas as entidades" },
                new Claim() { Id = 5003, ClaimPaiId = 5004, ClaimType = "AlterarMinhasEntidades", DefaultValue = "True", Descricao = "Alterar minhas entidades" },
                new Claim() { Id = 5004, ClaimPaiId = null, ClaimType = "VisualizarMinhasEntidades", DefaultValue = "True", Descricao = "Visualizar minhas entidades" },

                new Claim() { Id = 6001, ClaimPaiId = 6002, ClaimType = "AlterarTodosAnexos", DefaultValue = "True", Descricao = "Alterar todos os anexos" },
                new Claim() { Id = 6002, ClaimPaiId = null, ClaimType = "VisualizarTodasAnexos", DefaultValue = "True", Descricao = "Visualizar todos os anexos" },
                new Claim() { Id = 6003, ClaimPaiId = 6004, ClaimType = "AlterarMeusAnexos", DefaultValue = "True", Descricao = "Alterar meus anexos" },
                new Claim() { Id = 6004, ClaimPaiId = null, ClaimType = "VisualizarMeusAnexos", DefaultValue = "True", Descricao = "Visualizar meus anexos" },
                new Claim() { Id = 6005, ClaimPaiId = 6006, ClaimType = "AlterarTodosAnexosAssociados", DefaultValue = "True", Descricao = "Alterar todo anexos associados" },
                new Claim() { Id = 6006, ClaimPaiId = 6006, ClaimType = "VisualizarTodosAnexosAssociados", DefaultValue = "True", Descricao = "Visualizar todos anexos associados" },

                new Claim() { Id = 7001, ClaimPaiId = 7002, ClaimType = "AlterarTodosProcessamentos", DefaultValue = "True", Descricao = "Alterar todos os processamentos" },
                new Claim() { Id = 7002, ClaimPaiId = null, ClaimType = "VisualizarTodasProcessamentos", DefaultValue = "True", Descricao = "Visualizar todos os processamentos" },
                new Claim() { Id = 7003, ClaimPaiId = 7004, ClaimType = "AlterarMeusProcessamentos", DefaultValue = "True", Descricao = "Alterar meus processamentos" },
                new Claim() { Id = 7004, ClaimPaiId = null, ClaimType = "VisualizarMeusProcessamentos", DefaultValue = "True", Descricao = "Visualizar meus processamentos" }               

            };
        }
    }
}
