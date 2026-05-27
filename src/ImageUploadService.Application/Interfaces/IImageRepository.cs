using ImageUploadService.Domain.Entities;
namespace ImageUploadService.Application.Interfaces;

public interface IImageRepository
{
    Task<List<Image>> GetByUserIdAsync(string userId);
    Task<Image?> GetProfileImageAsync(string userId);
    Task AddAsync(Image image);
    Task DeleteAsync(string id);
}