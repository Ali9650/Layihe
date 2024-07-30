using Core.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.MSSQL_CONNECTION);
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Customer>().HasIndex(c => c.Fin).IsUnique();
    //    modelBuilder.Entity<Seller>().HasIndex(s => s.Fin).IsUnique();
    //    modelBuilder.Entity<Customer>().HasIndex(c => c.SeriaNumber).IsUnique();
    //    modelBuilder.Entity<Seller>().HasIndex(s => s.SeriaNumber).IsUnique();
    //    modelBuilder.Entity<Customer>().HasIndex(c => c.PhoneNumber).IsUnique();
    //    modelBuilder.Entity<Seller>().HasIndex(s => s.PhoneNumber).IsUnique();
    //    modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();
    //}
}
