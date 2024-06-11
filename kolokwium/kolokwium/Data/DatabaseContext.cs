using System.Data.Common;
using kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().HasData(new List<Item>
        {
            new Item
            {
                Id = 1,
                Name = "Sword",
                Weight = 15
            },
            new Item
            {
                Id = 2,
                Name = "Shield",
                Weight = 5
            },
            new Item
            {
                Id = 3,
                Name = "Helmet",
                Weight = 3
            }
        });

        modelBuilder.Entity<Title>().HasData(new List<Title>
        {
            new Title
            {
                Id = 1,
                Name = "Witcher"
            },
            new Title
            {
                Id = 2,
                Name = "Witcher 2"
            }
        });

        modelBuilder.Entity<Character>().HasData(new List<Character>
        {
            new Character
            {
                Id = 1,
                FirstName = "Witcher",
                LastName = "Idk",
                CurrentWeight = 100,
                MaxWeight = 120
            },
            new Character
            {
                Id = 2,
                FirstName = "Ciri",
                LastName = "smh",
                CurrentWeight = 50,
                MaxWeight = 60
            }
        });

        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
        {
            new CharacterTitle
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Parse("01-01-1990")
            },
            new CharacterTitle
            {
                CharacterId = 2,
                TitleId = 1,
                AcquiredAt = DateTime.Parse("01-01-1990")
            },
            new CharacterTitle
            {
                CharacterId = 1,
                TitleId = 2,
                AcquiredAt = DateTime.Parse("01-01-1990")
            },
            new CharacterTitle
            {
                CharacterId = 2,
                TitleId = 2,
                AcquiredAt = DateTime.Parse("01-01-1990")
            }
        });

        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
        {
            new Backpack
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 1
            },
            new Backpack
            {
                CharacterId = 1,
                ItemId = 2,
                Amount = 1
            },
            new Backpack
            {
                CharacterId = 1,
                ItemId = 3,
                Amount = 1
            },
            new Backpack
            {
                CharacterId = 2,
                ItemId = 2,
                Amount = 1
            },
            new Backpack
            {
                CharacterId = 2,
                ItemId = 3,
                Amount = 1
            }
        });
    }
}