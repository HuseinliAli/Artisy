using EventBus.Abstract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Console.IntegrationEvents.Events
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
