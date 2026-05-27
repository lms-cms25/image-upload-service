using ImageUploadService.Domain.Entities;
using ImageUploadService.Application.Interfaces;
using ImageUploadService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadService.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly ImageDbContext _context;

    public ImageRepository(ImageDbContext context)
    {
        _context = context;
    }

    public async Task<List<Image>> GetByUserIdAsync(string userId)
    {
        return await _context.Images
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Image?> GetProfileImageAsync(string userId)
    {
        return await _context.Images
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsProfileImage);
    }

    public async Task AddAsync(Image image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var image = await _context.Images.FindAsync(id);
        if (image != null)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}