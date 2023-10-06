using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class ArquivoFormatoAplicacao : BaseAplicacao<ArquivoFormato>, IArquivoFormatoAplicacao
    {
        public ArquivoFormatoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
        }
    }
}
