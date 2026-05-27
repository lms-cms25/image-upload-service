using ImageUploadService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadService.Infrastructure.Persistance;

public class ImageDbContext : DbContext
{
    public ImageDbContext(DbContextOptions<ImageDbContext> options)
        : base(options)
    {
    }

    public DbSet<Image> Images => Set<Image>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.Url).IsRequired();
            entity.Property(x => x.FileName).IsRequired();

            entity.Property(x => x.IsProfileImage).HasDefaultValue(false);
        });
    }
}