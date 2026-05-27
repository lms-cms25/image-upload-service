using ImageUploadService.Domain.Entities;

namespace ImageUploadService.Application.Interfaces;

public interface IImageRepository
{
    Task AddAsync(Image image);
    Task<List<Image>> GetByUserIdAsync(string userId);
}