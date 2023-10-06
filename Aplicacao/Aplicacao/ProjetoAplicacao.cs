using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;

namespace Aplicacao
{
    
    public class ProjetoAplicacao : BaseAplicacao<Projeto>, IProjetoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;        
        public ProjetoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;            
        }
        public Resultado<object> Cadastrar(string nome, int ambienteId, Usuario usuario)
        {
            var resultado = new Resultado<object>();

            if (string.IsNullOrEmpty(nome))
                resultado.Mensagem = "O nome n�o pode ser nulo ou vazio";
            else if (!usuario.Ambientes.Any(x => x.Id == ambienteId) && !usuario.Claims.Any(p => p.ClaimType == "AlterarTodosAmbientes"))
                resultado.Mensagem = "O ambiente informado n�o pertence a rede deste usu�rio";
            else
            {
                var projeto = new Projeto(nome, ambienteId, usuario.Id);
                Inserir(projeto);

                resultado.Retorno = projeto.Serializar();
                resultado.Sucesso = true;
            }

            return resultado;
        }
        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? length)
        {
            return _repositorio.Projeto.SelecionarProjetosFiltro(ambienteId, nome, dataDe, dataAte, excluido, start, length);
        }
        
    }
}
