using System;
using EventBus.Abstract;
using EventBus.Abstract.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationService.Console.Infrastructure;
using NotificationService.Console.IntegrationEvents.EventHandlers;
using NotificationService.Console.IntegrationEvents.Events;

namespace NotificationService.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            AddMailService(services);
            AddLogging(services);
            AddEvents(services);
            var sp = services.BuildServiceProvider();
            IEventBus eventBus = sp.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderPaymentFailIntegrationEvent, OrderPaymentFailIntegrationEventHandler>();
            eventBus.Subscribe<OrderPaymentSuccessIntegrationEvent, OrderPaymentSuccessIntegrationEventHandler>();

            System.Console.WriteLine("Notification is running");
            System.Console.ReadLine();
        }
        static void AddLogging(ServiceCollection services)
        {
            services.AddLogging(conf =>
            {
                conf.AddConsole();  
            });
        }
        static void AddMailService(ServiceCollection services)
        {
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
        static void AddEvents(ServiceCollection services)
        {
            services.AddTransient<OrderPaymentFailIntegrationEventHandler>();
            services.AddTransient<OrderPaymentSuccessIntegrationEventHandler>();
            services.AddSingleton<IEventBus>(sp =>
            {
                var config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "NotificationService",
                    EventBusType = EventBusType.RabbitMQ
                };
                return EventBusFactory.Create(config, sp);
            });
        }
    }
}
