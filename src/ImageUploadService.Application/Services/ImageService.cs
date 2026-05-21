using ImageUploadService.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ImageUploadService.Application.Services;

public class ImageService
{
    private readonly IBlobStorageService _blob;

    public ImageService(IBlobStorageService blob)
    {
        _blob = blob;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid file");

        return await _blob.UploadAsync(file);
    }
}