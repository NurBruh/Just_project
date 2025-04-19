using Microsoft.EntityFrameworkCore;

namespace Just_project.Models
{
    public class LocalizeDbContext : DbContext
    {
        public LocalizeDbContext(DbContextOptions<LocalizeDbContext> options)
            : base(options) { }

        public DbSet<LocalizedString> LocalizedStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocalizedString>().HasData(
                new LocalizedString { Id = 1, Key = "ProductDescription", Language = "en-US", Value = "Gaming PC" },
                new LocalizedString { Id = 2, Key = "ProductDescription", Language = "ru-RU", Value = "Игровой ПК" },
                new LocalizedString { Id = 3, Key = "ProductDescription", Language = "kk-KZ", Value = "Ойын компьютері" }
            );
        }
    }   
}
