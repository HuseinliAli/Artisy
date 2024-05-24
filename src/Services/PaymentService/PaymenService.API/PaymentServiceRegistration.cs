using EventBus.Abstract;
using EventBus.Abstract.Abstraction;
using EventBus.Factory;
using PaymenService.API.IntegrationEvents.EventHandlers;
using PaymenService.API.IntegrationEvents.Events;

namespace PaymenService.API
{
    public static class PaymentServiceRegistration
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                var config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "PaymentService",
                    EventBusType = EventBusType.RabbitMQ
                };
                return EventBusFactory.Create(config, sp);
            });

            services.AddTransient<OrderStartedIntegrationEventHandler>();
            return services;
        }
    }
}
