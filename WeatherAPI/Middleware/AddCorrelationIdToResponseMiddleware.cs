using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WeatherAPI.Middleware
{
    public class AddCorrelationIdToResponseMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public AddCorrelationIdToResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context
                .Response
                .Headers
                .Add(CorrelationIdHeaderName, context.TraceIdentifier);

            return _next(context);
        }
    }
}
