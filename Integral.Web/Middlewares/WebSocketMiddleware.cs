using System.Net.WebSockets;
using System.Threading.Tasks;
using Integral.Consumers;
using Microsoft.AspNetCore.Http;

namespace Integral.Middlewares
{
    internal sealed class WebSocketMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public WebSocketMiddleware(RequestDelegate requestDelegate) => this.requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext httpContext, Consumer<WebSocket> consumer)
        {
            WebSocketManager webSocketManager = httpContext.WebSockets;
            if (webSocketManager.IsWebSocketRequest)
            {
                consumer.Consume(await webSocketManager.AcceptWebSocketAsync());
            }
            else
            {
                await requestDelegate(httpContext);
            }
        }
    }
}
