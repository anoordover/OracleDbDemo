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
                });
        modelBuilder.Entity<Credit>()
            .ComplexProperty(c => c.OptionalPeriod,
                nb =>
                {
                    nb.Property(p => p.DatumVanaf)
                        .HasColumnName("uitkeringvanaf")
                        .IsRequired(false);
                    nb.Property(p => p.DatumTm)
                        .HasColumnName("uitkeringtm")
                        .IsRequired(false);
                });
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => c.Reference)
            .IsUnique();
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => new {c.DeclarationId, c.DeclarationReference})
            .IsUnique();
        modelBuilder.Entity<Contestation>()
            .HasIndex(c => new {c.CreditId, c.CreditReference})
            .IsUnique();
        modelBuilder.Entity<DummyEntity>()
            .Property(d => d.Id)
            .HasConversion<byte[]>();
    }

    public virtual DbSet<Credit> Credits { get; set; }
    public virtual DbSet<DummyEntity> Dummy { get; set; }

    public virtual DbSet<Declaration> Declarations { get; set; }
    
    public virtual DbSet<Contestation> Contestations { get; set; }
}