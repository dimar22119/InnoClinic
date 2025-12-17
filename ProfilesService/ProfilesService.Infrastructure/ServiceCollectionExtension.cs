using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Application.Interfaces.Services;
using ProfilesService.Application.Services;
using ProfilesService.Application.Validators.Doctors;
using ProfilesService.Application.Validators.Patients;
using ProfilesService.Application.Validators.Receptionists;
using ProfilesService.Infrastructure.Authentication;
using ProfilesService.Infrastructure.Persistance;
using ProfilesService.Infrastructure.Persistance.Repositories;
using System.Text;

namespace ProfilesService.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ProfilesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProfilesDatabase")));

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IReceptionistService, ReceptionistService>();

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDoctorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePatientRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePatientRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateReceptionistRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateReceptionistRequestValidator>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();
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

            services.AddAuthorization();
            return services;
        }
    }
}
