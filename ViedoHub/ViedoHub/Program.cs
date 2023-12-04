using ViedoHub.Channels;
using ViedoHub.Requests;
using ViedoHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<VideoProcessor>();
builder.Services.AddSingleton<FullHdVideoChannel>();
builder.Services.AddSingleton<UltraHdVideoChannel>();  
builder.Services.AddHostedService<FullHdVideoProcessor>();
builder.Services.AddHostedService<UltraHdVideoProcessor>();
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Video hub");

app.MapPost("/videos", async (ProcessVideo request, FullHdVideoChannel fullHdVideoChannel, UltraHdVideoChannel ultraHdVideoChannel) =>
{
    await fullHdVideoChannel.Channel.PublishAsync(request);
    await ultraHdVideoChannel.Channel.PublishAsync(request);
    return Results.Accepted();
});

app.Run();

