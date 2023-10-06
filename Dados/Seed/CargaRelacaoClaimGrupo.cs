using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaRelacaoClaimGrupo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            { 
                new RelacaoClaimGrupo() {ClaimId = 1001, ClaimGrupoId = 1},
                new RelacaoClaimGrupo() {ClaimId = 1002, ClaimGrupoId = 1},
                new RelacaoClaimGrupo() {ClaimId = 1003, ClaimGrupoId = 1},
                new RelacaoClaimGrupo() {ClaimId = 1004, ClaimGrupoId = 1},

                new RelacaoClaimGrupo() {ClaimId = 2001, ClaimGrupoId = 2},
                new RelacaoClaimGrupo() {ClaimId = 2002, ClaimGrupoId = 2},
                new RelacaoClaimGrupo() {ClaimId = 2003, ClaimGrupoId = 2},
                new RelacaoClaimGrupo() {ClaimId = 2004, ClaimGrupoId = 2},

                new RelacaoClaimGrupo() {ClaimId = 3001, ClaimGrupoId = 3},
                new RelacaoClaimGrupo() {ClaimId = 3002, ClaimGrupoId = 3},
                new RelacaoClaimGrupo() {ClaimId = 3003, ClaimGrupoId = 3},
                new RelacaoClaimGrupo() {ClaimId = 3004, ClaimGrupoId = 3},

                new RelacaoClaimGrupo() {ClaimId = 4001, ClaimGrupoId = 4},
                new RelacaoClaimGrupo() {ClaimId = 4002, ClaimGrupoId = 4},
                new RelacaoClaimGrupo() {ClaimId = 4003, ClaimGrupoId = 4},
                new RelacaoClaimGrupo() {ClaimId = 4004, ClaimGrupoId = 4},

                new RelacaoClaimGrupo() {ClaimId = 5001, ClaimGrupoId = 5},
                new RelacaoClaimGrupo() {ClaimId = 5002, ClaimGrupoId = 5},
                new RelacaoClaimGrupo() {ClaimId = 5003, ClaimGrupoId = 5},
                new RelacaoClaimGrupo() {ClaimId = 5004, ClaimGrupoId = 5},

                new RelacaoClaimGrupo() {ClaimId = 6001, ClaimGrupoId = 6},
                new RelacaoClaimGrupo() {ClaimId = 6002, ClaimGrupoId = 6},
                new RelacaoClaimGrupo() {ClaimId = 6003, ClaimGrupoId = 6},
                new RelacaoClaimGrupo() {ClaimId = 6004, ClaimGrupoId = 6},
            };
        }
    }
}
