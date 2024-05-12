using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using System.Data.SqlClient;
using Persistence.Entity;
using EFCore.BulkExtensions;

namespace DataAccessLayer.Repository
{
    public class BaseRepo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected readonly Entities _dbContext;
        private DbSet<TEntity> _dbSet;
        private string _coreConnectionString;
        private string _dataArchiveConnectionString;
        private string _sflCustomReportConnectionString;

        IConfigurationRoot configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        public BaseRepo(Entities dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<TEntity>();
            this._coreConnectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "Default");
            this._dataArchiveConnectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "DataArchive");
            this._sflCustomReportConnectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "Default");
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this._dbSet;
            //IQueryable<TEntity> query = this._dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByPKey(object key)
        {
            TEntity entity = this._dbSet.Find(key);
            if (entity != null)
            {
                if (this._dbContext.Entry(entity).State != EntityState.Added)
                {
                    this._dbContext.Entry(entity).State = EntityState.Detached;
                }
            }
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            //this._dbSet.AddRange(entities);
            this._dbContext.BulkInsert<TEntity>(entities.ToList());
        }

        public virtual void Delete(object key)
        {
            TEntity entityToDelete = this._dbSet.Find(key);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            //if (this._dbContext.Entry(entityToDelete).State == EntityState.Detached)
            //{
            //    this._dbSet.Attach(entityToDelete);
            //}
            this._dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            //this._dbSet.RemoveRange(entities);
            this._dbContext.BulkDelete<TEntity>(entities.ToList());
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            ////if (this._dbContext.Entry(entityToUpdate).State != EntityState.Detached)
            //var local = this._dbSet.Find(key);
            //if (local != null)
            //{
            //    this._dbContext.Entry(local).State = EntityState.Detached;
            //}
            //this._dbSet.Attach(entityToUpdate);
            this._dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            this._dbContext.BulkUpdate<TEntity>(entities.ToList());
        }

        public List<T> DataArchiveDBExecuteReader<T>(string query, Func<DbDataReader, T> map)
        {
            List<T> entities = null;

            using (SqlConnection conn = new SqlConnection(this._dataArchiveConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();

                    using (var result = command.ExecuteReader())
                    {
                        entities = new List<T>();
                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }
                    }
                }
            }

            return entities;
        }

        public void DataArchiveDBExecuteNonQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(this._dataArchiveConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<T> DBExecuteReader<T>(string query, Func<DbDataReader, T> map, CommandType commandType, List<SqlParameter> parameters)
        {
            List<T> entities = null;

            using (SqlConnection conn = new SqlConnection(_coreConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = commandType;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();

                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count(); i++)
                            command.Parameters.Add(parameters[i]);
                    }

                    using (var result = command.ExecuteReader())
                    {
                        entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }
                    }
                }
            }
            return entities;
        }

        public Int64 DBExecuteNonQuery(string query, CommandType commandType, List<SqlParameter> parameters = null)
        {
            Int64 result = 0;
            using (SqlConnection conn = new SqlConnection(_coreConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = commandType;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();

                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count(); i++)
                            command.Parameters.Add(parameters[i]);
                    }

                    result = command.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public List<T> CustomReportDBExecuteReader<T>(string query, Func<DbDataReader, T> map, CommandType commandType, List<SqlParameter> parameters)
        {
            List<T> entities = null;

            using (SqlConnection conn = new SqlConnection(this._sflCustomReportConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = commandType;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();

                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count(); i++)
                            command.Parameters.Add(parameters[i]);
                    }

                    using (var result = command.ExecuteReader())
                    {
                        entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }
                    }
                }
            }
            return entities;
        }

        public Int64 CustomReportDBExecuteNonQuery(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            Int64 result = 0;
            using (SqlConnection conn = new SqlConnection(this._sflCustomReportConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandText = query;
                    command.CommandType = commandType;
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();

                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count(); i++)
                            command.Parameters.Add(parameters[i]);
                    }

                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

    }
}
