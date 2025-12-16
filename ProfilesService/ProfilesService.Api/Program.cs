using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProfilesService.Api.Middleware;
using ProfilesService.Application.Validators.Doctors;
using ProfilesService.Application.Validators.Patients;
using ProfilesService.Application.Validators.Receptionists;
using ProfilesService.Infrastructure;
using ProfilesService.Infrastructure.Authentication;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDoctorRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePatientRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateReceptionistRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateReceptionistRequestValidator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
