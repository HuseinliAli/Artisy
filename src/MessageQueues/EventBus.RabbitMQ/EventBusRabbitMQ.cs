using EventBus.Abstract;
using EventBus.Abstract.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        private RabbitMQPersistenceConnection _rabbitMQPersistenceConnection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel _consumerChannel;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider):base(config, serviceProvider)
        {
            if(config.Connection is not null)
            {
                var connJson = JsonConvert.SerializeObject(EventBusConfig, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                _connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);

            }
            else
            {
                _connectionFactory = new ConnectionFactory();
            }
            _rabbitMQPersistenceConnection = new RabbitMQPersistenceConnection(_connectionFactory,config.ConnectionRetryCount);
            _consumerChannel = CreateConsumerChannel();

            EventSubscriptionManager.OnEventRemoved += SubscriptionManager_OnEventRemoved;
        }

        private void SubscriptionManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName: eventName);

            if (!_rabbitMQPersistenceConnection.IsConnected)
                _rabbitMQPersistenceConnection.TryConnect();

            _consumerChannel.QueueUnbind(
                queue:eventName,
                exchange:EventBusConfig.DefaultTopicName,
                routingKey:eventName);

            if (EventSubscriptionManager.IsEmpty)
                _consumerChannel.Close();
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!_rabbitMQPersistenceConnection.IsConnected)
                _rabbitMQPersistenceConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {

                });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            _consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");        

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() => 
            {
                var properties = _consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                //_consumerChannel.QueueDeclare(
                //    queue: GetSubscriptionName(eventName),
                //    durable: true,
                //    exclusive: false,
                //    autoDelete: false,
                //    arguments: null);

                //_consumerChannel.QueueBind(
                //    queue: GetSubscriptionName(eventName),
                //    exchange: EventBusConfig.DefaultTopicName,
                //    routingKey: eventName);

                _consumerChannel.BasicPublish(
                   exchange: EventBusConfig.DefaultTopicName,
                   routingKey: eventName,
                   mandatory: true,
                   basicProperties: properties,
                   body: body);
            });
        }

        public override void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        {
            var eventName = typeof(TIntegrationEvent).Name;
            eventName = ProcessEventName(eventName);

            if (!EventSubscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                if (!_rabbitMQPersistenceConnection.IsConnected)
                {
                    _rabbitMQPersistenceConnection.TryConnect();
                }

                _consumerChannel.QueueDeclare(
                    queue: GetSubscriptionName(eventName),
                    durable: true, 
                    exclusive: false,
                    autoDelete: false, 
                    arguments: null);

                _consumerChannel.QueueBind(
                    queue: GetSubscriptionName(eventName),
                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName);
            }

            EventSubscriptionManager.AddSubscription<TIntegrationEvent, TIntegrationEventHandler>();
            StartBasicConsume(eventName);
        }

        public override void UnSubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        {
            EventSubscriptionManager.RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_rabbitMQPersistenceConnection.IsConnected)
            {
                _rabbitMQPersistenceConnection.TryConnect();
            }

            var channel = _rabbitMQPersistenceConnection.CreateModel();

            channel.ExchangeDeclare(EventBusConfig.DefaultTopicName, "direct");
            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            if (_consumerChannel is not null)
            {
                var consumer = new EventingBasicConsumer(_consumerChannel);
                consumer.Received +=Consumer_Received;
                _consumerChannel.BasicConsume(
                    queue: GetSubscriptionName(eventName),
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {

            }
            _consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }
}
