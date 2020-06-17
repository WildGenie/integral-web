using Microsoft.AspNetCore.Mvc;

namespace Integral.Attributes
{
    public sealed class ControllerRouteAttribute : RouteAttribute
    {
        public ControllerRouteAttribute()
            : base("[controller]")
        {
        }
    }
}
