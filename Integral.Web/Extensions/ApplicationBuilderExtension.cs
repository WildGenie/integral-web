using Integral.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Integral.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseWebSocketMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseWebSockets();
            applicationBuilder.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
