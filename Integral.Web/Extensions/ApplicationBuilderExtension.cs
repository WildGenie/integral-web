using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;

namespace Integral.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void AddWebSockets(this IApplicationBuilder applicationBuilder, WebSocketOptions webSocketOptions)
        {
            applicationBuilder.UseWebSockets(webSocketOptions);
            applicationBuilder.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
