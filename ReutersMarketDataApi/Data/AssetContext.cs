using Microsoft.EntityFrameworkCore;
using ReutersMarketDataApi.Model;

namespace ReutersMarketDataApi.Data
{
    public class AssetContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Price> Prices { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("localDBConnection"));
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.DefaultTypeMapping<decimal>(builder => builder.HasPrecision(18, 8));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().ToTable("Assets");
            modelBuilder.Entity<Asset>().HasKey(b => b.Id);

            modelBuilder.Entity<Price>().ToTable("Prices");
        }
    }
}
