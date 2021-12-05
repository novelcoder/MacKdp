using Microsoft.EntityFrameworkCore;

namespace FickleDragon.MacKdp.DataContracts
{
	public class KdpDbContext : DbContext
	{
        public DbSet<ACXProduct> ACXProducts { get; set; }
        public DbSet<ACXRoyalty> ACXRoyalties { get; set; }
        public DbSet<ASINPageCount> ASINPageCounts { get; set; }
        public DbSet<BookEntry> BookEntries { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
		public DbSet<RoyaltyType> RoyaltyTypes { get; set; }
		public DbSet<WorkbookFile> WorkbookFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=/Users/jamesgreenwood/Databases/KdpSheet.db;");
            SQLitePCL.Batteries.Init();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACXProduct>().ToTable("ACXProduct");
            modelBuilder.Entity<ACXRoyalty>().ToTable("ACXRoyalty");
            modelBuilder.Entity<ASINPageCount>().ToTable("ASINPageCount");
            modelBuilder.Entity<BookEntry>().ToTable("BookEntry");
            modelBuilder.Entity<ExchangeRate>().ToTable("ExchangeRate");
            modelBuilder.Entity<RoyaltyType>().ToTable("RoyaltyType");
            modelBuilder.Entity<WorkbookFile>().ToTable("WorkbookFile");
        }
    }
}