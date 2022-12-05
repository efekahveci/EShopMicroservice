using Order.Application.Models;
using Order.Infrastructure.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();





app.Run();
