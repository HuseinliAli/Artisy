using EventBus.Abstract.Abstraction;
using EventBus.Abstract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstract.SubManagers
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;
        public Func<string, string> EventNameGetter;
        public bool IsEmpty => !_handlers.Keys.Any();

        public InMemoryEventBusSubscriptionManager(Func<string, string>eventNameGetter)
        {
            _handlers = new();
            _eventTypes = new ();
            EventNameGetter = eventNameGetter;
        }

        public void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var eventName = GetEventKey<TIntegrationEvent>();

            AddSubscription(typeof(TIntegrationEventHandler), eventName);

            if (!_eventTypes.Contains(typeof(TIntegrationEvent)))
                _eventTypes.Add(typeof(TIntegrationEvent));
        }

        private void AddSubscription(Type handlerType, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
                _handlers.Add(eventName, new());

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }

        public void Clear()
            => _handlers.Clear();

        public string GetEventKey<T>()
            => EventNameGetter(typeof(T).Name);

        public Type GetEventTypeByName(string eventName)
            => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent
            => GetHandlersForEvent(GetEventKey<TIntegrationEvent>());

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
            => _handlers[eventName];

        public bool HasSubscriptionsForEvent<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent
            => HasSubscriptionsForEvent(GetEventKey<TIntegrationEvent>());

        public bool HasSubscriptionsForEvent(string eventName)
            => _handlers.ContainsKey(eventName);

        public void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var handlerToRemove = FindSubscriptionToRemove<TIntegrationEvent, TIntegrationEventHandler>();
            var eventName = GetEventKey<TIntegrationEvent>();
            RemoveHandler(eventName, handlerToRemove);  
        }

        private void RemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            if(subsToRemove is not null)
            {
                _handlers[eventName].Remove(subsToRemove);

                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventName is not null)
                        _eventTypes.Remove(eventType);

                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(string eventName)
            => OnEventRemoved?.Invoke(this, eventName);

        private SubscriptionInfo FindSubscriptionToRemove<TIntegrationEvent, TIntegrationEventHandler>()
            => FindSubscriptionToRemove(GetEventKey<TIntegrationEvent>(), typeof(TIntegrationEventHandler));
        
        private SubscriptionInfo FindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
                return null;

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
