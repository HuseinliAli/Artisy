using EventBus.Abstract.Events;

namespace EventBus.RabbitMQ.Test
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public int Id { get; set; }
        public OrderCreatedIntegrationEvent(int id)
        {
            Id = id;
        }
    }

}