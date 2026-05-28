using ImageUploadService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadService.Api.Controllers;

[ApiController]
[Route("api/images")]
[Authorize]
public class ImagesController : ControllerBase
{
    private readonly ImageService _service;

    public ImagesController(ImageService service)
    {
        _service = service;
    }

    // Upload image (optionally as profile image).
    [HttpPost]
    public async Task<IActionResult> Upload(
        [FromForm] IFormFile file,
        [FromForm] bool isProfileImage = false)
    {
        var userId = GetUserId();

        var result = await _service.UploadAsync(file, userId, isProfileImage);

        return Ok(result);
    }

    // Replace profile image (deletes old one if exists)
    [HttpPost("profile")]
    public async Task<IActionResult> ReplaceProfile([FromForm] IFormFile file)
    {
        var userId = GetUserId();

        var result = await _service.ReplaceProfileImageAsync(file, userId);

        return Ok(result);
    }

    // Get images for the authenticated user
    [HttpGet]
    public async Task<IActionResult> GetMyImages()
    {
        var userId = GetUserId();

        var result = await _service.GetByUserIdAsync(userId);

        return Ok(result);
    }

    // Get images for any user (admin only)
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var result = await _service.GetByUserIdAsync(userId);

        return Ok(result);
    }

    // Helper method to get user ID from JWT token.
    private string GetUserId()
    {
        return User.FindFirst("sub")?.Value
            ?? throw new UnauthorizedAccessException("UserId not found in token");
    }
}