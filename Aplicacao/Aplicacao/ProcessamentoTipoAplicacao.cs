using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class ProcessamentoTipoAplicacao : BaseAplicacao<ProcessamentoTipo>, IProcessamentoTipoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        public ProcessamentoTipoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
        }
    }
}
