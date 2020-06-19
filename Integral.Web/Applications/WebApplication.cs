using Integral.Contexts;
using Integral.Extensions;
using Integral.Options;
using Integral.Roles;
using Integral.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
            serviceCollection.AddDbContext<IdentityContext>(Configure);
            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>(Configure)
                             .AddEntityFrameworkStores<IdentityContext>()
                             .AddDefaultTokenProviders();

            serviceCollection.AddSignalR(Configure);
            serviceCollection.AddRazorPages(Configure);
            serviceCollection.AddControllers(Configure);
            serviceCollection.AddEmailSender(Configure);
            serviceCollection.AddExternalAuth(Configure);
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage(ConfigureDeveloperExceptionPage());
            }
            else
            {
                applicationBuilder.UseExceptionHandler(ConfigureExceptionHandler());
                applicationBuilder.UseHsts();
            }

            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseEndpoints(Configure);
            applicationBuilder.UseFileServer(ConfigureFileServer());
        }

        protected abstract void Configure(DbContextOptionsBuilder dbContextOptionsBuilder);

        protected abstract void Configure(EmailSenderOptions emailSenderOptions);

        protected abstract void Configure(ExternalAuthOptions externalAuthOptions);

        protected virtual void Configure(IdentityOptions identityOptions)
        {
        }

        protected virtual void Configure(HubOptions hubOptions)
        {
        }

        protected virtual void Configure(RazorPagesOptions razorPagesOptions)
        {
        }

        protected virtual void Configure(MvcOptions mvcOptions)
        {
        }

        protected virtual void Configure(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapRazorPages();
            endpointRouteBuilder.MapControllers();
        }

        protected virtual DeveloperExceptionPageOptions ConfigureDeveloperExceptionPage() => new DeveloperExceptionPageOptions();

        protected virtual ExceptionHandlerOptions ConfigureExceptionHandler() => new ExceptionHandlerOptions { ExceptionHandlingPath = "/Error" };

        protected virtual FileServerOptions ConfigureFileServer() => new FileServerOptions();
    }
}