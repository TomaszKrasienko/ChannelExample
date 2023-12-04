using ViedoHub.Channels;

namespace ViedoHub.Services;

public class UltraHdVideoProcessor : BackgroundService
{
    private readonly UltraHdVideoChannel _channel;
    private readonly VideoProcessor _processor;

    public UltraHdVideoProcessor(UltraHdVideoChannel channel, VideoProcessor processor)
    {
        _channel = channel;
        _processor = processor;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var request in _channel.Channel.SubscribeAsync(stoppingToken))
        {
            await _processor.ProcessAsync(request with {Quality = "4k"});
        }
    }
}