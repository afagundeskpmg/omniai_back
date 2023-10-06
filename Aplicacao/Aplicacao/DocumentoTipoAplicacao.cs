using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    public class DocumentoTipoAplicacao : BaseAplicacao<DocumentoTipo>, IDocumentoTipoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        public DocumentoTipoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
        }
        public Resultado<object> Cadastrar(string nome, int ambienteId, Usuario usuario) 
        {
            var resultado = new Resultado<object>();

            if (string.IsNullOrEmpty(nome))
                resultado.Mensagem = "O nome n�o pode ser nulo ou vazio";
            else if (!usuario.PertenceAoAmbiente(ambienteId) && !usuario.Claims.Any(p => p.ClaimType == "AlterarTodosDocumentosTipo"))
                resultado.Mensagem = "O ambiente informado n�o pertence a rede deste usu�rio";
            else
            {
                var documentoTipo = new DocumentoTipo(nome, ambienteId, usuario.Id);
                Inserir(documentoTipo);

                resultado.Retorno = documentoTipo.Serializar();
                resultado.Sucesso = true;

            }

            return resultado;
        }

        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? length)
        {
            return _repositorio.DocumentoTipo.SelecionarProjetosFiltro(ambienteId, nome, dataDe, dataAte, excluido, start, length);
        }
    }
}
