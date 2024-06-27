using Microsoft.EntityFrameworkCore;
using Judaica_2.Models;
namespace Judaica_2.Data
{
    public class Ctx : DbContext
    {
        public Ctx()
        {
            this.Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
            if (Categories.Any() || Items.Any() || Prices.Any()) return;

            string parentCategoryName = "חנות יודאיקה הכי שווה בעולם";
            string[] subCateogries = [
                "ספרי תורה",
                "תפילין",
                "מזוזות",
                "כיפות",
                "סידורים"
                ];
            // add categories

            Category category = new Category { Name = parentCategoryName };
            Categories.Add(category);
            int[] prices = { 100, 200, 300, 400, 500 };
            /*
            subCateogries.ToList().ForEach(c =>
            {
                Category category1 = new Category { Name = c, Parent = category };
                Categories.Add(category1);
                int i = 0;
                subCateogries
                    .ToList()
                    .ForEach((cName) =>
                            {
                                Item item = new Item
                                {
                                    Name = cName + i++,
                                    Category = category1
                                };
                                Items.Add(item);

                                Prices.AddRange(prices.Select(p => new Price { MyPrice = p, Item = item }));
                            });
            });
            */
            // sub-cateogries
            List<Category> sb = subCateogries.Select(c => new Category { Name = c, Parent = category }).ToList();
            Categories.AddRange(sb);

            List<Item> items = sb.Select(c => new Item { Name = c.Name, Category = c }).ToList();
            Items.AddRange(items);

            List<Price> itemPrices = items.SelectMany(i => prices.Select(p => new Price { MyPrice = p, Item = i })).ToList();
            Prices.AddRange(itemPrices);

            SaveChanges();
        }

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
