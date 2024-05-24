using BasketService.API.Application.IntegrationEvents.EventHandlers;
using BasketService.API.Application.Repositories;
using BasketService.API.Application.Services;
using BasketService.API.Infrastructure.Repositories;
using BasketService.API.Infrastructure.Services;
using EventBus.Abstract;
using EventBus.Abstract.Abstraction;
using EventBus.Factory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace BasketService.API.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddRemoteAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]))
                 };
             });

            services.AddAuthorization();
            return services;
        }

        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
        {
            var redisConfiguration = ConfigurationOptions.Parse(configuration["RedisSettings:cStr"], true);
            redisConfiguration.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConfiguration);
        }

        public static IServiceCollection AddRedisRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => sp.ConfigureRedis(configuration));
            return services;
        }

        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            
            services.AddSingleton<IEventBus>(sp =>
            {
                var config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = " BasketService",
                    EventBusType = EventBusType.RabbitMQ
                };
                return EventBusFactory.Create(config, sp);
            });
            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            return services;
        }

        public static IServiceCollection AddRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, RedisBasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            return services;
        }
    }
}
