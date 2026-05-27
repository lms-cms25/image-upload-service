using ImageUploadService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadService.Api.Controllers;

[ApiController]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private readonly ImageService _service;

    public ImagesController(ImageService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, string userId, bool isProfileImage = false)
    {
        var result = await _service.UploadAsync(file, userId, isProfileImage);
        return Ok(result);
    }

    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetProfileImage(string userId)
    {
        var images = await _service.GetByUserIdAsync(userId);

        var profileImage = images
            .FirstOrDefault(x => x.IsProfileImage);

        if (profileImage == null)
            return NotFound();

        return Ok(profileImage);
    }
}