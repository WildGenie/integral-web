using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Integral.Applications
{
    public abstract class WebApplication : Application
    {
        protected WebApplication(IConfiguration configuration) => Configuration = configuration;

        protected IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection serviceCollection)
        {
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                this.Configure(developerExceptionPageOptions);
                applicationBuilder.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                ExceptionHandlerOptions exceptionHandlerOptions = new ExceptionHandlerOptions();
                this.Configure(exceptionHandlerOptions);
                applicationBuilder.UseExceptionHandler(exceptionHandlerOptions);
            }
        }

        protected virtual void Configure(DeveloperExceptionPageOptions developerExceptionPageOptions)
        {
        }

        protected virtual void Configure(ExceptionHandlerOptions exceptionHandlerOptions)
        {
            exceptionHandlerOptions.ExceptionHandlingPath = "/Error";
        }
    }
}