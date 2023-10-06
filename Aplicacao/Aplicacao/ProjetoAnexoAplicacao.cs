using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class ProjetoAnexoAplicacao : BaseAplicacao<ProjetoAnexo>, IProjetoAnexoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        public ProjetoAnexoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
        }
    }
}
