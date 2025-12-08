using EduMentor.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EduMentor.Persistence.Context;

public class EduMentorDbContext(DbContextOptions<EduMentorDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<PasswordReset> PasswordResets => Set<PasswordReset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();

            entity.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Role>(entity => { entity.HasKey(r => r.Id); });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.HasKey(st => st.Id);

            entity.HasOne(st => st.User)
                .WithMany()
                .HasForeignKey(st => st.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<PasswordReset>().HasQueryFilter(u => !u.IsDeleted);
    }
}
