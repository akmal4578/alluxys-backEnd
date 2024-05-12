using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Persistence.Entity.Security;  //changes


namespace Persistence.EntityConfiguration.Security
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Table Override

            ///Define table schema
            //builder.ToTable(builder.Metadata.GetTableName(), SchemaConstant.SecurityDbSchema);
            builder.ToTable("User", SchemaConstant.SecurityDbSchema);

            ///Define primary key
            builder.HasKey(k => k.IdUser);

            ///Define unique constraint
            builder.HasIndex(k => k.Name).IsUnique();

            #endregion


            #region Property Configuration

            ///Set auto generate value for primary key
            builder.Property(p => p.IdUser)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.AliasName)
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(250);

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .HasMaxLength(50);

            builder.Property(p => p.DisplayName)
                .HasMaxLength(50);

            builder.Property(p => p.Password)
                .HasMaxLength(150);

            builder.Property(p => p.PhoneNo)
                .HasMaxLength(50);

            #endregion


            #region Relationship

            ///Configure the relationship

            builder.HasOne(r => r.User1)
                .WithMany(r => r.Users1)
                .HasForeignKey(k => k.IdUserCreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.User2)
                .WithMany(r => r.Users2)
                .HasForeignKey(k => k.IdUserUpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.RefObjectState1)
                .WithMany(r => r.Users1)
                .HasForeignKey(k => k.IdRefObjectState)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Seed Data

            builder.HasData(
                new User { IdUser = Guid.NewGuid(), Name = "Admin", AliasName = "Admin", Description = "Admin user", IdRefObjectState = 1, Password = "admin123", FirstName = "Admin", MiddleName = "Bin", LastName = "Admin", DisplayName = "Admin", Email = "admin@example.com", PhoneNo = "0123456789", IdUserCreatedBy = null, IdUserUpdatedBy = null, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            #endregion

        }
    }
}
