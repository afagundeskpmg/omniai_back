using Dominio.Entidades;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Aplicacao.Interface
{
    public interface IAnexoAplicacao : IBaseAplicacao<Anexo>
    {
        Resultado<Anexo> Cadastrar(int anexoTipoId, int anexoArquivoTipoId, object obj, Stream stream, string arquivoNome, Usuario usuarioLogado, Dictionary<string, object>? parametrosAdicionais , bool pularValidacoes);
        Resultado<CloudBlob> SelecionarCloudBlob(string accountName, string accountKey, string containerName, string caminhoArquivo);
        Resultado<Anexo> SelecionarParaAlteracao(int? anexoId, Usuario? usuarioLogado, string? objetoId);
    }
}
