using Microsoft.EntityFrameworkCore;

namespace Just_project.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<PcModel> Pcs { get; set; }
        public DbSet<PcTranslationModel> PcTranslations { get; set; }
        //public DbSet<CreatePcViewModel> CreatePcViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PcTranslationModel>()
                .HasOne(pt => pt.PcModel)
                .WithMany(p => p.Translations)
                .HasForeignKey(pt => pt.PcId);
        }

    }
    
 }
