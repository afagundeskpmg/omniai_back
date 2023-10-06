using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class AnexoArquivoTipoAplicacao : BaseAplicacao<AnexoArquivoTipo>, IAnexoArquivoTipoAplicacao
    {
        public AnexoArquivoTipoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
        }
    }
}
