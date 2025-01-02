using Domain.Events;
using MassTransit;

namespace Bus.Consumers
{
    public class ProductAddedEventConsumer() : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Event:{context.Message.Id} {context.Message.Name} {context.Message.Price}");
            return Task.CompletedTask;
        }
    }
}
