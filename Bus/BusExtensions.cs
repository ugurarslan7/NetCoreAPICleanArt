using Application.ServiceBus;
using Bus.Consumers;
using Domain.Const;
using Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bus
{
    public static class BusExtensions
    {
        public static void AddBusExt(this IServiceCollection services,IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();   

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url),h => { });
                    cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName,
                        e => { e.ConfigureConsumer<ProductAddedEventConsumer>(context); });
                });
            });

        }
    }
}
