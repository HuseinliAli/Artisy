using EventBus.Abstract.Events;

namespace PaymenService.API.IntegrationEvents.Events
{
    public class OrderPaymentFailIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public string Message { get; set; }
        public OrderPaymentFailIntegrationEvent(int orderId, string message)
        {
            OrderId = orderId;
            Message = message;
        }
    }
}
