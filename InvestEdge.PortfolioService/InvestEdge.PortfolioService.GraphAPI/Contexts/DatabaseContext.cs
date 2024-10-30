using InvestEdge.PortfolioService.GraphAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestEdge.PortfolioService.GraphAPI.Contexts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureDeleted();
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