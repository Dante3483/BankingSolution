using BankingSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Infrastructure.Contexts
{
    /// <summary>
    /// Represents the database context for the banking solution.
    /// </summary>
    public class BankingDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for accounts.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for customers.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for transactions.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankingDbContext"/> class.
        /// </summary>
        public BankingDbContext() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankingDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options) { }

        /// <summary>
        /// Configures the database context options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder to be used for configuration.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.Combine("..", "BankingSolution.Infrastructure", "Bank.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        /// <summary>
        /// Configures the entity models for the database context.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entity models.</param>
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
