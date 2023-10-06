using Dados.Contexto;
using Dados.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Dados.Repositorio
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly ContextoBase _contexto;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepositorio(ContextoBase contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<TEntity>();
        }

        #region Dados
        public TEntity? SelecionarPorId(object id)
        {
            return _dbSet.Find(id);
        }

        public IList<TEntity> SelecionarTodos()
        {
            return _dbSet.ToList();
        }

        public void Inserir(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Deletar(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void DeletarTodos(ICollection<TEntity> obj)
        {
            _dbSet.RemoveRange(obj);
        }

        public void InserirTodos(ICollection<TEntity> obj)
        {
            _dbSet.AddRange(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _contexto.Entry(obj).State = EntityState.Modified;
        }

        public void Recarregar(TEntity obj)
        {
            _contexto.Entry(obj).Reload();
        }

        public IEnumerable<TEntity> SelecionarTodos(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool noTracking = false)
        {
            IQueryable<TEntity> query = _contexto.Set<TEntity>();

            if (noTracking)
                query = query.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public TEntity? SelecionarFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                return _contexto.Set<TEntity>().FirstOrDefault(filter);
            }
            else
            {
                return _contexto.Set<TEntity>().FirstOrDefault();
            }
        }

        #endregion

        public void InserirTabelaDataTable(DataTable dataTable, string tabela, bool incluirColunaId = true)
        {
            using (SqlConnection connection = new SqlConnection(_contexto.Database.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BulkCopyTimeout = 300;
                        bulkCopy.DestinationTableName = tabela;

                        if (incluirColunaId)
                        {
                            DataColumn ColId = dataTable.Columns.Add("Id", Type.GetType("System.String"));
                            ColId.SetOrdinal(0);

                        }

                        bulkCopy.WriteToServer(dataTable);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
