using System;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;

namespace Integral.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseWebSockets(this IApplicationBuilder applicationBuilder, Action<WebSocket> accept, WebSocketOptions webSocketOptions)
        {
            applicationBuilder.UseWebSockets(webSocketOptions);
            applicationBuilder.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    accept(await context.WebSockets.AcceptWebSocketAsync());
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
