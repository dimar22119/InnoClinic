using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Application.Interfaces.Services;
using ProfilesService.Application.Services;
using ProfilesService.Infrastructure.Persistance;
using ProfilesService.Infrastructure.Persistance.Repositories;

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
    }
}
