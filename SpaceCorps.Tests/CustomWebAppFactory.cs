using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SpaceCorps.Business.Db;

namespace SpaceCorps.Tests;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    private readonly string _remoteDbConnectionString = "Server=rorycraft.com,1433;Database=SpaceCorpsTest;User Id=sa;Password=Password_2_Change_4_Real_Cases_&;TrustServerCertificate=true";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:DefaultConnection", _remoteDbConnectionString);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<DatabaseContext>>();
            services.RemoveAll<DatabaseContext>();
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(_remoteDbConnectionString);
            });

            // Ensure database is created and updated to latest migration
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                dbContext.Database.Migrate();
            }
        });
    }
}