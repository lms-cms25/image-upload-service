using ImageUploadService.Application.Services;
using ImageUploadService.Application.Interfaces;
using ImageUploadService.Infrastructure.BlobStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DI
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();