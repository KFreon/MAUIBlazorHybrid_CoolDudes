using Microsoft.EntityFrameworkCore;

namespace Database;

public class AppDbContext : DbContext
{
    public DbSet<CoolDude> CoolDudes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CoolDude>()
            .HasKey(x => x.Id);
    }
}

public class CoolDude
{
    /// <summary>
    /// DB generated
    /// </summary>
    public int Id { get; private set; }
    public string Name { get; private set; }
    public bool AreTheyACoolDude { get; private set; }

    public CoolDude(string name, bool areTheyACoolDude)
    {
        Name = name;
        AreTheyACoolDude = areTheyACoolDude;
    }
}
