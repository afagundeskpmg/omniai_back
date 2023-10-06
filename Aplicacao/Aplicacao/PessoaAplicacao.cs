using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class PessoaAplicacao : BaseAplicacao<Pessoa>, IPessoaAplicacao
    {
        public PessoaAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
        }
    }
}
