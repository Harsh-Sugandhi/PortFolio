using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Project> Projects => Set<Project>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(project => project.ID);

                entity.Property(project => project.Title)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(project => project.Description)
                    .IsRequired();

                entity.Property(project => project.TechStack)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(project => project.GitHubUrl)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(project => project.LiveUrl)
                    .HasMaxLength(200)
                    .IsRequired();
            });
        }

    }

}
