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
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var url = await _service.UploadAsync(file);
        return Ok(new { url });
    }
}