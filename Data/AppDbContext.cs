using Api_Teste0002.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Teste0002.Data
{
    public class AppDbContext : DbContext{
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

    }
}