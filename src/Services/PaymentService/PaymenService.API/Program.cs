using EventBus.Abstract.Abstraction;
using PaymenService.API;
using PaymenService.API.IntegrationEvents.EventHandlers;
using PaymenService.API.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEvents();
builder.Services.AddLogging(c => { c.AddConsole(); });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
