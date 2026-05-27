using Microsoft.AspNetCore.Http;

namespace ImageUploadService.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadAsync(IFormFile file);
}