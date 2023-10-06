using Aplicacao.Aplicacao;
using Microsoft.Extensions.Configuration;

namespace Teste
{
    public static class AddAplicacaoRepositorioApp
    {
        public static void Executar(params string[] nomes)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            using (UnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
            {
                _aplicacao.ConfigurarEntidadesNoUnitOfWork(nomes);
            }
        }
    }
}
