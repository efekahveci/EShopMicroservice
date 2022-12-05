using EventBus.RabbitMQ.Common;
using MassTransit;
using Microsoft.OpenApi.Models;
using Order.API.EventBusConsumer;
using Order.Application;
using Order.Application.Models;
using Order.Infrastructure;
using Order.Infrastructure.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ApplicationServices();
builder.Services.InfrastructureServices(builder.Configuration);


// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});

// General Configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<BasketCheckoutConsumer>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    var efe = scope.ServiceProvider.GetService(typeof(EmailSettings)) as EmailSettings;

    var service = new EmailService(efe);


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
