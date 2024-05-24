using EventBus.Abstract.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationService.Console.Infrastructure;
using NotificationService.Console.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Console.IntegrationEvents.EventHandlers
{
   
    class OrderPaymentFailIntegrationEventHandler(ILogger<OrderPaymentFailIntegrationEventHandler> logger ,IEmailSender emailSender) : IIntegrationEventHandler<OrderPaymentFailIntegrationEvent>
    {
        public Task Handle(OrderPaymentFailIntegrationEvent @event)
        {
            var message = $"{@event.OrderId} ORDER FAILED!!";
            emailSender.SendEmailAsync("FAILED ORDER",message).GetAwaiter();
            logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
