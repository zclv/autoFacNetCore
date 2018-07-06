using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace writeIpMiddleWare
{
    public static class RequestIPExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
