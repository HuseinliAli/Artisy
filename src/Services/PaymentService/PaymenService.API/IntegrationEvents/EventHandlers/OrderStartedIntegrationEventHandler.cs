using EventBus.Abstract.Abstraction;
using EventBus.Abstract.Events;
using PaymenService.API.IntegrationEvents.Events;

namespace PaymenService.API.IntegrationEvents.EventHandlers
{
    public class OrderStartedIntegrationEventHandler(IConfiguration configuration, IEventBus eventBus, ILogger<OrderStartedIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        public Task Handle(OrderStartedIntegrationEvent @event)
        {
            string keyword = "PaymentSuccess";
            bool paymentSuccessFlag = configuration.GetValue<bool>(keyword);
            IntegrationEvent paymentEvent = paymentSuccessFlag
                ? new OrderPaymentSuccessIntegrationEvent(@event.OrderId)
                : new OrderPaymentFailIntegrationEvent(@event.OrderId, "PaymentService not allowed this order") ;

            logger.LogInformation($"OrderStartedIntegrationEventHandler in PaymentService is PaymentSuccess: {paymentSuccessFlag}, orderId: {@event.OrderId}");

            eventBus.Publish(paymentEvent);

            return Task.CompletedTask;
        }
    }
}
