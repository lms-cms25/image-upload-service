using ImageUploadService.Application.Interfaces;
using ImageUploadService.Domain.Entities;
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

    public async Task AddAsync(Image image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Image>> GetByUserIdAsync(string userId)
    {
        return await _context.Images
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}