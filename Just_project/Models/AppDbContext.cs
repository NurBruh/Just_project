using Microsoft.EntityFrameworkCore;

namespace Just_project.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Pc> Pcs { get; set; }
        public DbSet<PcTranslation> PcTranslations { get; set; }
        //public DbSet<CreatePcViewModel> CreatePcViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PcTranslation>()
                .HasOne(pt => pt.Pc)
                .WithMany(p => p.Translations)
                .HasForeignKey(pt => pt.PcId);
        }

    }
    
 }
