using Azure.Storage.Blobs;
using ImageUploadService.Application.Interfaces;
using ImageUploadService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ImageUploadService.Infrastructure.BlobStorage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _container;

    public BlobStorageService(IConfiguration config)
    {
        var conn = config["Azure:BlobConnection"];
        var container = config["Azure:ContainerName"];

        _container = new BlobContainerClient(conn, container);
        _container.CreateIfNotExists();
    }

    public async Task<Image> UploadAsync(IFormFile file, string userId)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var blob = _container.GetBlobClient(fileName);

        using var stream = file.OpenReadStream();
        await blob.UploadAsync(stream, overwrite: true);

        return new Image
        {
            UserId = userId,
            Url = blob.Uri.ToString(),
            FileName = fileName,
            IsProfileImage = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public async Task DeleteAsync(string fileName)
    {
        var blob = _container.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }
}