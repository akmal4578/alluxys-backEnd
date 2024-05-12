using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using refObjectState=Persistence.Entity.RefObjectState;


namespace Persistence.EntityConfiguration.RefObjectState
{
    class RefObjectStateConfiguration : IEntityTypeConfiguration<refObjectState.RefObjectState>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<refObjectState.RefObjectState> builder)
        {
            #region Table Override

            ///Define table schema
            //builder.ToTable(builder.Metadata.GetTableName(), SchemaConstant.SecurityDbSchema);
            builder.ToTable("RefObjectState", SchemaConstant.DefaultDbSchema);

            ///Define primary key
            builder.HasKey(k => k.IdRefObjectState);

            ///Define unique constraint
            builder.HasIndex(k => k.Name).IsUnique();

            #endregion

            #region Property Configuration

            ///Set auto generate value for primary key

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.AliasName)
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(250);

            #endregion

            #region Relationship

            ///Configure the relationship

            builder.HasOne(r => r.User1)
                .WithMany(r => r.RefObjectStates1)
                .HasForeignKey(k => k.IdUserCreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.User2)
                .WithMany(r => r.RefObjectStates2)
                .HasForeignKey(k => k.IdUserUpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Seed Data

            builder.HasData(
                new refObjectState.RefObjectState { IdRefObjectState=1, Name="Active", AliasName="Active", Description="Active", IsDefault=true}
                ,new refObjectState.RefObjectState { IdRefObjectState = 2, Name = "Terminated", AliasName = "Terminated", Description = "Terminated", IsDefault = false }
                ,new refObjectState.RefObjectState { IdRefObjectState = 3, Name = "Frozen", AliasName = "Frozen", Description = "Frozen", IsDefault = false }
                );

            #endregion
        }
    }
}
