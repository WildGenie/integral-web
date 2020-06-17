using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Integral.Applications
{
    public class WebApplication : Application
    {
        public WebApplication(IConfiguration configuration) => Configuration = configuration;

        protected IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddRazorPages();
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                applicationBuilder.UseExceptionHandler("/Error");
                applicationBuilder.UseHsts();
            }

            applicationBuilder.UseRouting();
            applicationBuilder.UseFileServer();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseEndpoints(Configure);
        }

        protected virtual void Configure(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder.MapRazorPages();
    }
}