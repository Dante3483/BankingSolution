using BankingSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Contexts
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Bank.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(customer =>
            {
                customer.HasKey(c => c.Id);
                customer.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
                customer.Property(c => c.LastName).HasMaxLength(50).IsRequired();
                customer.Property(c => c.Email).HasMaxLength(100).IsRequired();
                customer.Property(c => c.Address).HasMaxLength(100).IsRequired();
                customer.Property(c => c.PhoneNumber).HasMaxLength(15).IsRequired();
                customer.Property(c => c.DateOfBirth).IsRequired();
            });

            modelBuilder.Entity<Account>(account =>
            {
                account.HasKey(a => a.Id);
                account
                    .HasOne(a => a.Customer)
                    .WithMany()
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
                account.Property(a => a.AccountNumber).HasMaxLength(50).IsRequired();
                account.Property(a => a.CurrentBalance).IsRequired().HasColumnType("decimal(18,2)");
                account.Property(a => a.DateOpened).IsRequired();
                account.Property(a => a.DateClosed);
            });

            modelBuilder.Entity<Transaction>(transaction =>
            {
                transaction.HasKey(t => t.Id);
                transaction.Property(t => t.TransactionType).IsRequired();
                transaction.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
                transaction.Property(t => t.TransactionDate).IsRequired();
                transaction.Property(t => t.TransactionStatus).IsRequired();
                transaction
                    .HasOne(t => t.Account)
                    .WithMany()
                    .HasForeignKey(t => t.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
                transaction
                    .HasOne(t => t.DestinationAccount)
                    .WithMany()
                    .HasForeignKey(t => t.DestinationAccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
