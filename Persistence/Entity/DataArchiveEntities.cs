using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace Persistence.Entity
{
    public class DataArchiveEntities : DbContext
    {
        private IConfigurationRoot _appConfig;

        public DataArchiveEntities()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json");
            this._appConfig = configBuilder.Build();
            this.ChangeTracker.LazyLoadingEnabled = false;

            int intAutoCreateDatabase = this._appConfig.GetValue<int>("AutoCreateDatabase");
            if (intAutoCreateDatabase == 1) this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=;user id=sa;password=abc@123;");
            optionBuilder.UseSqlServer(ConfigurationExtensions.GetConnectionString(this._appConfig, "DataArchive"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
