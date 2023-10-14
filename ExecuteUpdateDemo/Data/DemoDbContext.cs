using Microsoft.EntityFrameworkCore;

namespace ExecuteUpdateDemo.Data;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Declaration>()
            .HasIndex(d => d.Reference)
            .IsUnique();
        modelBuilder.Entity<Credit>()
            .HasIndex(c => new {c.DeclarationId, c.DeclarationReference})
            .IsUnique();
        modelBuilder.Entity<Credit>()
            .HasIndex(c => c.Reference)
            .IsUnique();
        modelBuilder.Entity<Credit>()
            .OwnsOne(c => c.Period,
                nb =>
                {
                    nb.Property(p => p.StartDate)
                        .IsRequired();
                    nb.Property(p => p.EndDate)
                        .IsRequired();
                }).Navigation(c => c.Period)
            .IsRequired();
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => c.Reference)
            .IsUnique();
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => new {c.DeclarationId, c.DeclarationReference})
            .IsUnique();
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => new {c.CreditId, c.CreditReference})
            .IsUnique();
    }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Declaration> Declarations { get; set; }
    
    public virtual DbSet<Contestation> Contestations { get; set; }
}