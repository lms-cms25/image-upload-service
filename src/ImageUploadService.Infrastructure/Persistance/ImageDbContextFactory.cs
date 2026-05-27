using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ImageUploadService.Infrastructure.Persistance;

public class ImageDbContextFactory : IDesignTimeDbContextFactory<ImageDbContext>
{
    public ImageDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ImageDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ImageUploadDb;Trusted_Connection=True;");

        return new ImageDbContext(optionsBuilder.Options);
    }
}