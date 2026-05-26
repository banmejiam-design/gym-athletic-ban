using GymManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Context;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<GymClass> GymClasses => Set<GymClass>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Membership> Memberships => Set<Membership>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
            e.Property(m => m.LastName).IsRequired().HasMaxLength(100);
            e.Property(m => m.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(m => m.Email).IsUnique();
            e.Property(m => m.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Trainer>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            e.Property(t => t.LastName).IsRequired().HasMaxLength(100);
            e.Property(t => t.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(t => t.Email).IsUnique();
            e.Property(t => t.Specialization).HasMaxLength(200);
        });

        modelBuilder.Entity<GymClass>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(g => g.Name).IsRequired().HasMaxLength(150);
            e.Property(g => g.Description).HasMaxLength(500);
            e.HasOne(g => g.Trainer)
             .WithMany(t => t.GymClasses)
             .HasForeignKey(g => g.TrainerId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // N:M between Member and GymClass through Enrollment
        modelBuilder.Entity<Enrollment>(e =>
        {
            e.HasKey(en => en.Id);
            e.HasOne(en => en.Member)
             .WithMany(m => m.Enrollments)
             .HasForeignKey(en => en.MemberId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(en => en.GymClass)
             .WithMany(g => g.Enrollments)
             .HasForeignKey(en => en.GymClassId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(en => new { en.MemberId, en.GymClassId }).IsUnique();
        });

        // 1:N between Member and Membership
        modelBuilder.Entity<Membership>(e =>
        {
            e.HasKey(ms => ms.Id);
            e.Property(ms => ms.Price).HasColumnType("decimal(10,2)");
            e.HasOne(ms => ms.Member)
             .WithMany(m => m.Memberships)
             .HasForeignKey(ms => ms.MemberId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
