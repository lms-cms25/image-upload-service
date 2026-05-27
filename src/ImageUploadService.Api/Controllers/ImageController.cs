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
    public async Task<IActionResult> Upload(IFormFile file, string userId)
    {
        var result = await _service.UploadAsync(file, userId);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var images = await _service.GetByUserIdAsync(userId);
        return Ok(images);
    }
}