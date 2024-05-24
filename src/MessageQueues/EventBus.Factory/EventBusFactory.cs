using EventBus.Abstract;
using EventBus.Abstract.Abstraction;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
            => config.EventBusType switch
            {
                //azure service bus not implemented yet
                EventBusType.RabbitMQ => new EventBusRabbitMQ(config, serviceProvider),
                _ => new EventBusRabbitMQ(config, serviceProvider)
            };
    }
}
