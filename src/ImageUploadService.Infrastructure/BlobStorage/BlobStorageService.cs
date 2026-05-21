using Azure.Storage.Blobs;
using ImageUploadService.Application.Interfaces;
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

    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var blob = _container.GetBlobClient(fileName);

        using var stream = file.OpenReadStream();
        await blob.UploadAsync(stream, overwrite: true);

        return blob.Uri.ToString();
    }
}