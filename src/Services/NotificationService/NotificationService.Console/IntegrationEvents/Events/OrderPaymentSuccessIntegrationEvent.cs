using EventBus.Abstract.Events;

namespace NotificationService.Console.IntegrationEvents.Events
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public OrderPaymentSuccessIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
