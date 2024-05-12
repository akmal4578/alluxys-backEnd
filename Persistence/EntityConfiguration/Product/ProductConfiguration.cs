using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Entity.Product;

namespace Persistence.EntityConfiguration.Product
{
    class ProductConfiguration : IEntityTypeConfiguration<Entity.Product.Product>
    {
        public void Configure(EntityTypeBuilder<Entity.Product.Product> builder)
        {
            #region Table Override

            ///Define table schema
            builder.ToTable("Product", SchemaConstant.ProductDbSchema);

            ///Define primary key
            builder.HasKey(k => k.IdProduct);

            ///Define unique constraint
            builder.HasIndex(k => new { k.Name }).IsUnique();

            #endregion

            #region Property Configuration

            ///Set auto generate value for primary key
            builder.Property(p => p.IdProduct)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.AliasName)
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(250);

            #endregion

            #region Relationship

            //Configure relationship

            builder.HasOne(r => r.User1)
                .WithMany(r => r.Products1)
                .HasForeignKey(k => k.IdUserCreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.User2)
                .WithMany(r => r.Products2)
                .HasForeignKey(k => k.IdUserUpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.RefObjectState1)
                .WithMany(r => r.Products1)
                .HasForeignKey(k => k.IdRefObjectState)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Seed Data

            builder.HasData(
                new Entity.Product.Product { IdProduct=Guid.NewGuid(), Name="Product 1", AliasName="Product 1", Description="Product 1", IdRefObjectState=1}
                );

            #endregion
        }
    }
}
