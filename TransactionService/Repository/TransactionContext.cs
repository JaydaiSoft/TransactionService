using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.Entity;

namespace TransactionServices.Repository
{
    public class TransactionContext : DbContext, ITransactionContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) 
            : base(options)
        {

        }

        public override DatabaseFacade Database => base.Database;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder.ApplyConfiguration(new TransactionsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public class TransactionsConfiguration : IEntityTypeConfiguration<Transactions>
        {
            public void Configure(EntityTypeBuilder<Transactions> builder)
            {
                // Set configuration for table
                builder.ToTable("Transactions", "dbo");

                // Set key for entity
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Id).HasColumnType("int").IsRequired();
                builder.Property(p => p.TransactionId).HasColumnType("varchar(50)").IsRequired();
                builder.Property(p => p.Amount).HasColumnType("decimal(18, 2)").IsRequired();
                builder.Property(p => p.CurrencyCode).HasColumnType("varchar(3)").IsRequired();
                builder.Property(p => p.TransactionDate).HasColumnType("datetime").IsRequired();
                builder.Property(p => p.Status).HasColumnType("char(1)").IsRequired();
            }
        }

        public DbSet<Transactions> TransactionEntity { get; set; }
    }
}
