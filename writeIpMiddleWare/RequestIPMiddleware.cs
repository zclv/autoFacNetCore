using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace writeIpMiddleWare
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestIPMiddleware(RequestDelegate next,ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<RequestIPMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("User IP:"+context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
        }
    }
}
