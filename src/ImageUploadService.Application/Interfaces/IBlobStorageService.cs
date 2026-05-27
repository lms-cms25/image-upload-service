using Microsoft.AspNetCore.Http;
using ImageUploadService.Domain.Entities;

namespace ImageUploadService.Application.Interfaces;

public interface IBlobStorageService
{
    Task<Image> UploadAsync(IFormFile file, string userId);
    Task DeleteAsync(string fileName);
}