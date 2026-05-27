using ImageUploadService.Application.Interfaces;
using ImageUploadService.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ImageUploadService.Application.Services;

public class ImageService
{
    private readonly IBlobStorageService _blob;
    private readonly IImageRepository _repo;

    public ImageService(IBlobStorageService blob, IImageRepository repo)
    {
        _blob = blob;
        _repo = repo;
    }

    public async Task<Image> UploadAsync(IFormFile file, string userId)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid file");

        var url = await _blob.UploadAsync(file);

        var image = new Image
        {
            UserId = userId,
            Url = url,
            FileName = file.FileName,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(image);

        return image;
    }

    public async Task<List<Image>> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
}