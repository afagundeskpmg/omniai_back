using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class ProcessamentoStatusAplicacao : BaseAplicacao<ProcessamentoStatus>, IProcessamentoStatusAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        public ProcessamentoStatusAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
        }
    }
}
