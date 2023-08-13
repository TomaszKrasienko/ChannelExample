using ViedoHub.Requests;
using ViedoHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<VideoProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Video hub");

app.MapPost("/videos", async (ProcessVideo request, VideoProcessor processor) =>
{
    await processor.ProcessAsync(request);
    return Results.Ok();
});

app.Run();

