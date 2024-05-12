using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace DataAccessLayer.Repository
{
    public interface IRepo<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);

        void Delete(object key);

        void DeleteRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByPKey(object key);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);

        void UpdateRange(IEnumerable<TEntity> entities);

        public List<T> DBExecuteReader<T>(string query, Func<DbDataReader, T> map, CommandType commandType, List<SqlParameter> parameters);

        public Int64 DBExecuteNonQuery(string query, CommandType commandType, List<SqlParameter> parameters = null);

        public List<T> CustomReportDBExecuteReader<T>(string query, Func<DbDataReader, T> map, CommandType commandType, List<SqlParameter> parameters);


    }
}
