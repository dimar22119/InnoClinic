using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProfilesService.Infrastructure.Persistance
{
    public class ProfilesDbContextFactory : IDesignTimeDbContextFactory<ProfilesDbContext>
    {
        public ProfilesDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ProfilesService.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ProfilesDbContext>();
            var connectionString = configuration.GetConnectionString("ProfilesDatabase");
            Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString);

            return new ProfilesDbContext(optionsBuilder.Options);
        }
    }
}
