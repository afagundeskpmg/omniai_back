using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Aplicacao
{
    public class ProcessamentoAplicacao : BaseAplicacao<Processamento>, IProcessamentoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        public ProcessamentoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
        }
        public void FinalizarProcessamento(ProcessamentoStatusEnum processamentoStatusEnum, string processamentoId, string log)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE Processamento SET InicioProcessamentoEm = (CASE WHEN InicioProcessamentoEm IS NOT NULL THEN InicioProcessamentoEm ELSE SYSDATETIMEOFFSET() AT TIME ZONE 'E. South America Standard Time' END),UltimaAlteracaoEm = SYSDATETIMEOFFSET() AT TIME ZONE 'E. South America Standard Time', FimProcessamentoEm = SYSDATETIMEOFFSET() AT TIME ZONE 'E. South America Standard Time', ProcessamentoStatusId = " + (int)processamentoStatusEnum);
            sql.AppendLine("WHERE Id = " + processamentoId);

            if (!string.IsNullOrEmpty(log))
                sql.AppendLine("INSERT INTO ProcessamentoLog (Descricao, ProcessamentoId, CriadoEm) VALUES ('" + log.Replace("'", "") + "', " + processamentoId + ", SYSDATETIMEOFFSET() AT TIME ZONE 'E. South America Standard Time')");

            _repositorio.ExecutarQuery(sql.ToString());
        }

        

    }
}
