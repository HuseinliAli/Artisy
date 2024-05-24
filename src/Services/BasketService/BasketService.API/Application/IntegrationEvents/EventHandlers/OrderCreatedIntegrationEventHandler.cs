using BasketService.API.Application.IntegrationEvents.Events;
using BasketService.API.Application.Repositories;
using EventBus.Abstract.Abstraction;

namespace BasketService.API.Application.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler(IBasketRepository repository, ILogger<OrderCreatedIntegrationEvent> logger) : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            logger.LogInformation("----- Handling integration event: {IntegrationEventId} at BasketService.Api - ({@IntegrationEvent})", @event.Id, @event);

            repository.DeleteBasketAsync(@event.UserId).GetAwaiter();
            return Task.CompletedTask;
        }
    }
}
