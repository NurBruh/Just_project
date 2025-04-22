using Microsoft.EntityFrameworkCore;

namespace Just_project.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<PcModel> Pcs { get; set; }
        public DbSet<PcTranslationModel> PcTranslations { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<BlogTranslationModel> BlogTranslations { get; set; }
        public DbSet<ComponentsModel> Components { get; set; }
        public DbSet<ComponentsTranslationModel> ComponentsTranslations { get; set; }
        //public DbSet<CreatePcViewModel> CreatePcViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PcTranslationModel>()
                .HasOne(pt => pt.PcModel)
                .WithMany(p => p.PcTranslations)
                .HasForeignKey(pt => pt.PcId);

            modelBuilder.Entity<BlogTranslationModel>()
                .HasOne(bt => bt.BlogModel)
                .WithMany(b => b.BlogTranslations)
                .HasForeignKey(bt => bt.BlogId);
            modelBuilder.Entity<ComponentsTranslationModel>()
                .HasOne(ct => ct.ComponentsModel)
                .WithMany(c => c.ComponentsTranslations)
                .HasForeignKey(ct => ct.ComponentsId);
        }

    }
    
 }
