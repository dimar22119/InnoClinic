using Auth.Application.Users.Commands;
using Auth.Application.Users.Validators;
using Auth.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
