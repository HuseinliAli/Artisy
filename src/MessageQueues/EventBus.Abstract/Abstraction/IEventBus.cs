using EventBus.Abstract.Events;

namespace EventBus.Abstract.Abstraction
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent: IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void UnSubscribe<TIntegrationEvent, TIntegrationEventHandler>()
          where TIntegrationEvent : IntegrationEvent
          where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;
    }
}
