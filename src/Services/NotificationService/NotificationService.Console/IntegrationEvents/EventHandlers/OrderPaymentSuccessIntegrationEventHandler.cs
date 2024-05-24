using EventBus.Abstract.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationService.Console.Infrastructure;
using NotificationService.Console.IntegrationEvents.Events;

namespace NotificationService.Console.IntegrationEvents.EventHandlers
{
    class OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEventHandler> logger ,IEmailSender emailSender) : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
        {
            var message = $"Order Payment Success with OrderId: {@event.OrderId}";
            emailSender.SendEmailAsync("SUCCESS ORDER", message).GetAwaiter();
            logger.LogInformation(message);

            return Task.CompletedTask;
        }
    }
}
