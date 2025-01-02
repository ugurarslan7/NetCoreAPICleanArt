using Domain.Events;

namespace Application.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T @event,CancellationToken cancellationToken=default) where T : IEventOrMessage;

        Task SendAsyns<T>(T message,string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage;
    }
}
