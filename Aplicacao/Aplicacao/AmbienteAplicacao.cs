using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class AmbienteAplicacao : BaseAplicacao<Ambiente>, IAmbienteAplicacao
    {
        public AmbienteAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
        }
    }
}
