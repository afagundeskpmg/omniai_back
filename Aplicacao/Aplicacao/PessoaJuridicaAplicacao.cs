using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class PessoaJuridicaAplicacao : BaseAplicacao<PessoaJuridica>, IPessoaJuridicaAplicacao
    {
        public PessoaJuridicaAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
        }
    }
}
