using InvestEdge.PortfolioService.GraphAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestEdge.PortfolioService.GraphAPI.Contexts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        //Database.Migrate();
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Holding> Holdings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasData(new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Cruise",
                Email = "tom.cruise@gmail.com"
            }, new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Hiddleston",
                Email = "tom.hiddleston@gmail.com"
            }, new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Hardy",
                Email = "tom.hardy@gmail.com"
            });

        modelBuilder.Entity<Asset>()
            .HasData(new()
            {
                Id = Guid.NewGuid(),
                Name = "Bitcoin",
                Description = "First crypto",
                Type = Enums.AssetType.Crypto,
                Price = 75000
            }, new()
            {
                Id = Guid.NewGuid(),
                Name = "Ethereum",
                Description = "Second crypto and first well developed blockchain infrastructure",
                Type = Enums.AssetType.Crypto,
                Price = 5000
            }, new()
            {
                Id = Guid.NewGuid(),
                Name = "Dogecoin",
                Description = "Just a memecoin pumped by Elon Musk",
                Type = Enums.AssetType.Crypto,
                Price = 10
            });

        // Configure one-to-one relationship between User and Holding using UserId as foreign key
        modelBuilder.Entity<Holding>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Holding>(h => h.UserId);

        // Configure one-to-one relationship between Asset and Holding using AssetId as foreign key
        modelBuilder.Entity<Holding>()
            .HasOne<Asset>()
            .WithOne()
            .HasForeignKey<Holding>(h => h.AssetId);
    }
}