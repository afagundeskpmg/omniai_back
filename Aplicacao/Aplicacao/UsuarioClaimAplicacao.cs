using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class UsuarioClaimAplicacao : BaseAplicacao<UsuarioClaim>, IUsuarioClaimAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        public UsuarioClaimAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
        }
    }
}
