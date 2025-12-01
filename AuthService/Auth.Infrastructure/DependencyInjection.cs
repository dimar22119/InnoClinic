using Auth.Application.Services;
using Auth.Domain.Repositories;
using Auth.Infrastructure.Persistance;
using Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AuthDatabase")));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
