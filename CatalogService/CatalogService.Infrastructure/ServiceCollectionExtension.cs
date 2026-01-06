using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Services;
using CatalogService.Infrastructure.Persistance;
using CatalogService.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CatalogDb")));

            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddScoped<ISpecializationManager, SpecializationManager>();
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
