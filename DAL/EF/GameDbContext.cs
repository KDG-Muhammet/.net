using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreManagement.BL.Domain;

namespace StoreManagement.DAL.EF;

public class GameDbContext : DbContext
{
    
    public DbSet<Game> Games { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<GameStore> GameStores { get; set; }

  
    public GameDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=../../../../AppDatabase.db.sqlite");

        }
        
        optionsBuilder.LogTo((logMessage) =>  Debug.WriteLine(logMessage),LogLevel.Information ); 
    }


    public bool CreateDatabase(bool dataBase)
    {
        if (dataBase)
            Database.EnsureDeleted();
        
        return Database.EnsureCreated();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /* GameStore <-> Game */
        modelBuilder.Entity<GameStore>()
            .HasOne(gamestore => gamestore.Game)
            .WithMany(game=> game.Store)
            .HasForeignKey("FK_Game")
            .IsRequired();
        /* GameStore <-> Store */
        modelBuilder.Entity<GameStore>()
            .HasOne(gamestore => gamestore.Store)
            .WithMany(store => store.Game)
            .HasForeignKey("FK_Store")
            .IsRequired();
        /* AuthorBook */
        modelBuilder.Entity<GameStore>()
            .HasKey("FK_Game", "FK_Store");
    }
}