using MassTransit;

namespace RabbitStudy.Bus;

internal interface IPublishBus
{
    Task PublishAsync<T>(T message, CancellationToken ct) where T : class;
}

internal class PublishBus : IPublishBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PublishBus(IPublishEndpoint publish)
    {
        _publishEndpoint = publish;
    }
    
    public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
    {
        return _publishEndpoint.Publish(message);
    }
}