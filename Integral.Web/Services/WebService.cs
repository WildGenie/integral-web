using System.Threading.Tasks;
using Integral.Applications;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Integral.Services
{
    public abstract class WebService : Service
    {
        public static async Task Main(string[] arguments) => await Run<WebApplication>(arguments);

        protected static async Task Run<Initializer>(string[] arguments)
            where Initializer : WebApplication
        {
            await Host.CreateDefaultBuilder(arguments)
                    .ConfigureWebHostDefaults(Configure<Initializer>)
                    .Build()
                    .RunAsync();
        }

        private static void Configure<Initializer>(IWebHostBuilder webHostBuilder)
            where Initializer : WebApplication
        {
            //webHostBuilder.UseKestrel();
            webHostBuilder.UseStartup<Initializer>();
        }
    }
}