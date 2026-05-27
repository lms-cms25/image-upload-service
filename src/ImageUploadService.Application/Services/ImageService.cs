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

    // UPLOAD NORMAL IMAGE
    public async Task<Image> UploadAsync(IFormFile file, string userId, bool isProfileImage)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid file");

        var uploaded = await _blob.UploadAsync(file, userId);

        uploaded.IsProfileImage = isProfileImage;

        await _repo.AddAsync(uploaded);

        return uploaded;
    }

    // REPLACE PROFILE IMAGE
    public async Task<Image> ReplaceProfileImageAsync(IFormFile file, string userId)
    {
        var existing = await _repo.GetByUserIdAsync(userId);

        var oldProfile = existing.FirstOrDefault(x => x.IsProfileImage);

        if (oldProfile != null)
        {
            await _blob.DeleteAsync(oldProfile.FileName);
            await _repo.DeleteAsync(oldProfile.Id);
        }

        var newImage = await _blob.UploadAsync(file, userId);
        newImage.IsProfileImage = true;

        await _repo.AddAsync(newImage);

        return newImage;
    }

    // GET USER IMAGES
    public async Task<List<Image>> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
}