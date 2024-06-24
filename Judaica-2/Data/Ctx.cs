using Microsoft.EntityFrameworkCore;
using Judaica_2.Models;
namespace Judaica_2.Data
{
    public class Ctx : DbContext
    {
        //private static DbContextOptions GetOptions(string connectionString)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=AEK\\SQLEXPRESS;initial catalog=judaica;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .Property(p => p.MyPrice)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
