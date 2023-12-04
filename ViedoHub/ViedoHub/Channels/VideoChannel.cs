using System.Threading.Channels;
using ViedoHub.Requests;

namespace ViedoHub.Channels;

public class VideoChannel
{
    private readonly Channel<ProcessVideo> _channel = Channel.CreateUnbounded<ProcessVideo>();

    public ValueTask PublishAsync(ProcessVideo request) => _channel.Writer.WriteAsync(request);

    public IAsyncEnumerable<ProcessVideo> SubscribeAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);
}