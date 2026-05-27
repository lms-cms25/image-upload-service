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

    [HttpPost("replace-profile")]
    public async Task<IActionResult> ReplaceProfile(IFormFile file, string userId)
    {
        var result = await _service.ReplaceProfileImageAsync(file, userId);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var result = await _service.GetByUserIdAsync(userId);
        return Ok(result);
    }
}