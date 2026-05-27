using System;
using System.Collections.Generic;
using System.Text;

namespace ImageUploadService.Domain.Entities;

public class Image
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsProfileImage { get; set; } = false;
}