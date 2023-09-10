using Microsoft.OpenApi.Models;
using Novorender.API;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Novorender API", Version = "v1" });
});

var app = builder.Build();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Map the zip endpoint
app.MapGet("/api/zip/{files:int}", async (HttpContext context, int files) =>
{
    context.Response.ContentType = "application/octet-stream";
    context.Response.Headers.Add("Content-Disposition", "attachment; filename=test.zip");

    using var memoryStream = new MemoryStream();
    await Zip.WriteAsync(memoryStream, files, context.RequestAborted);

    memoryStream.Seek(0, SeekOrigin.Begin);
    await memoryStream.CopyToAsync(context.Response.Body);
}).WithDescription("Download a zip file with random content.");

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Novorender API v1");
        c.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
    });
}

app.Run();

