using ImageUploadService.Application.Interfaces;
using ImageUploadService.Application.Services;
using ImageUploadService.Infrastructure.BlobStorage;
using ImageUploadService.Infrastructure.Persistance;
using ImageUploadService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ImageDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new Exception("Missing DefaultConnection connection string");

    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();