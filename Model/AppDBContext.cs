using Microsoft.EntityFrameworkCore;
using TestBankingAPI.Contracts.DBModel;

namespace TestBankingAPI.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Customer Table
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 1,
                Name = "Arisha Barron"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 2,
                Name = "Branden Gibson"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 3,
                Name = "Rhonda Church"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 4,
                Name = "Georgina Hazel"
            });
        }
    }
}
