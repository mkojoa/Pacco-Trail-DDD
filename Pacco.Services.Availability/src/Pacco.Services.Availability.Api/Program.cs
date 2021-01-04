using System.Threading.Tasks;
using Convey;
using Convey.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Availability.Application;
using Pacco.Services.Availability.Infrastructure;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core;

namespace Pacco.Services.Availability.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                    .Build()
                    .RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                      .ConfigureServices(services =>
                       {
                           services.AddControllers();
                           services
                              .AddConvey()
                              .AddApplication()
                              .AddInfrastructure()
                              .Build();
                       })
                      .Configure(app =>
                       {
                           app.UseInfrastructure();
                           UpdateDatabase(app);
                           app.UseRouting()
                              .UseEndpoints(e => e.MapControllers());
                       });

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EfCoreContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}