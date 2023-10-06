using System.Data;
using System.Linq.Expressions;

namespace Dados.Interface
{
    public interface IBaseRepositorio<TEntity> where TEntity : class
    {
        #region Dados
        TEntity? SelecionarPorId(object id);
        IList<TEntity> SelecionarTodos();
        void Inserir(TEntity entity);
        void Atualizar(TEntity obj);
        void Deletar(TEntity obj);
        void Recarregar(TEntity obj);
        IEnumerable<TEntity> SelecionarTodos(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool noTracking = false);
        TEntity? SelecionarFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null);
        void DeletarTodos(ICollection<TEntity> obj);
        void InserirTodos(ICollection<TEntity> obj);
        #endregion

        void InserirTabelaDataTable(DataTable dataTable, string tabela, bool incluirColunaId = true);
    }
}
