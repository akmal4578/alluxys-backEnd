using DataAccessLayer.Repository.Product;
using DataAccessLayer.Repository.RefObjectState;
using DataAccessLayer.Repository.Security;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Entities _dbContext;

        public UnitOfWork()
        {
            this._dbContext = new Entities();
            this.RefObjectStates = new RefObjectState(this._dbContext);
            this.Users = new User(this._dbContext);
            this.Products = new Product(this._dbContext);
        }

        #region Repository Property

        public IRefObjectState RefObjectStates { get; private set; }
        public IUser Users { get; private set; }
        public IProduct Products { get; private set; }

        #endregion

        #region Method

        public void CreateIOTagArchivedTable(string tableName)
        {
            DataArchiveEntities DataArchiveDBContext = new DataArchiveEntities();

            //string query = string.Format("IF NOT EXISTS (SELECT name FROM [SRTCoreDataArchive].sys.tables WHERE xtype='U' AND name='{0}') BEGIN CREATE TABLE [IOTag].[{0}] ([IdIOTagArchived][bigint] IDENTITY(1, 1) NOT NULL, [IdIOTag] [bigint] NOT NULL, [ArchivedValue] [nvarchar](50) NULL, [IdRefObjectState] [bigint] NOT NULL, [CreatedDate] [datetime2](7) NULL, " +
            //    "CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED ([IdIOTagArchived] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON [PRIMARY]; " +
            //    "ALTER TABLE [IOTag].[{0}] WITH CHECK ADD CONSTRAINT [FK_{0}_IOTag] FOREIGN KEY([IdIOTag]) REFERENCES [IOTag].[IOTag]([IdIOTag]); ALTER TABLE [IOTag].[{0}] CHECK CONSTRAINT [FK_{0}_IOTag]; " +
            //    "ALTER TABLE [IOTag].[{0}] WITH CHECK ADD CONSTRAINT [FK_{0}_RefObjectState_IdRefObjectState] FOREIGN KEY([IdRefObjectState]) REFERENCES [dbo].[RefObjectState]([IdRefObjectState]);" +
            //    "ALTER TABLE [IOTag].[{0}] CHECK CONSTRAINT [FK_{0}_RefObjectState_IdRefObjectState]; CREATE NONCLUSTERED INDEX [IX_{0}_CreatedDate] ON [IOTag].[{0}] ( [CreatedDate] DESC, [IdRefObjectState] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]; END", tableName);
            //this._dbContext.Database.ExecuteSqlRaw(query);

            string query = string.Format("IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name='IOTag') EXEC ('CREATE SCHEMA [IOTag] AUTHORIZATION [dbo]'); IF NOT EXISTS (SELECT name FROM sys.tables WHERE type='U' AND name='{0}') BEGIN CREATE TABLE [IOTag].[{0}] ([IdIOTagArchived][bigint] IDENTITY(1, 1) NOT NULL, [IdIOTag] [bigint] NOT NULL, [ArchivedValue] [nvarchar](50) NULL, [IdRefObjectState] [bigint] NOT NULL, [CreatedDate] [datetime2](7) NULL, [DataCollectedDate] [datetime2](7) NULL, " +
                "CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED ([IdIOTagArchived] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON [PRIMARY]; " +
                "CREATE NONCLUSTERED INDEX [IX_{0}_CreatedDate] ON [IOTag].[{0}] ( [CreatedDate] DESC, [IdRefObjectState] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]; END", tableName);
            DataArchiveDBContext.Database.ExecuteSqlRaw(query);
            DataArchiveDBContext.Dispose();
        }

        public void CreateEntityTypeNameAttribute(string tableName)
        {
            var dbName = "";
            var SchemaName = "";

            List<string> SchemaList = new List<string>();
            using (var _db = new Entities())
            {
                dbName = new System.Data.SqlClient.SqlConnectionStringBuilder(_db.Database.GetDbConnection().ConnectionString).InitialCatalog;

                var schemas = _db.Model.GetEntityTypes().Select(t => t.GetSchema()).Distinct().ToList();
                SchemaList = schemas;
            }

            foreach (var schema in SchemaList)
            {
                if (tableName.Contains(schema))
                    SchemaName = schema;
            }

            string query = string.Format("IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name='{1}') EXEC ('CREATE SCHEMA [{1}] AUTHORIZATION [dbo]'); " +
                "IF NOT EXISTS (SELECT name FROM sys.tables WHERE type='U' AND name='{0}') BEGIN CREATE TABLE [{1}].[{0}] " +
                "([IdEntityTypeNameAttribute] [bigint] IDENTITY(1,1) NOT NULL,[IdEntityTypeAttribute][uniqueidentifier] NOT NULL,[IdObject] [nvarchar](250) NULL,[AttributeValue] [nvarchar](250) NULL,[IdUserCreatedBy] [uniqueidentifier] NULL,[CreatedDate] [datetime2](7) NULL,[IdUserUpdatedBy] [uniqueidentifier] NULL,[UpdatedDate] [datetime2](7) NULL,[IdRefObjectState] [bigint] NOT NULL," +
                "CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([IdEntityTypeNameAttribute] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]; END", tableName, SchemaName);
            this._dbContext.Database.ExecuteSqlRaw(query);
        }


        public int Complete()
        {
            return this._dbContext.SaveChanges();
            return 0;
        }


        /// <summary>
        /// Use this method to commit the changes and 
        /// log the Added, Modified & Deleted of Entity State into AuditLog
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public int Complete(Guid userId, bool enableChangeTracker = true)
        //{
        //    return this._dbContext.SaveChanges(userId, enableChangeTracker);
        //}

        //public async Task<int> Save()
        //{
        //    return await this._dbContext.SaveChangesAsync();
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
