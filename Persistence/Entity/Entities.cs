using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Persistence.Entity.Security;
using refObjectState = Persistence.Entity.RefObjectState;
using refObjectStateConfig = Persistence.EntityConfiguration.RefObjectState;
using Persistence.EntityConfiguration.Product;
using Persistence.EntityConfiguration.Security;

namespace Persistence.Entity
{
    public class Entities : DbContext
    {
        private IConfigurationRoot _appConfig;

        public Entities() //Entities(DbContextOptions<Entities> option) : base(option)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json");
            this._appConfig = configBuilder.Build();
            this.ChangeTracker.LazyLoadingEnabled = false;

            int intAutoCreateDatabase = this._appConfig.GetValue<int>("AutoCreateDatabase");
            if (intAutoCreateDatabase == 1) this.Database.Migrate();

            DataArchiveEntities dataArchiveEntities = new DataArchiveEntities();
            dataArchiveEntities.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=;user id=sa;password=abc@123;");
            optionBuilder.UseSqlServer(ConfigurationExtensions.GetConnectionString(this._appConfig, "Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new refObjectStateConfig.RefObjectStateConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet

        public DbSet<User> Users { set; get; }
        public DbSet<refObjectState.RefObjectState> RefObjectStates { set; get; }
        public DbSet<Product.Product> Products { get; set; }

        #endregion
    }
}
