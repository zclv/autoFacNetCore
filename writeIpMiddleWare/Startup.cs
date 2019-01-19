using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace writeIpMiddleWare
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            app.Use(next => new RequestDelegate(async context => {
                await context.Response.WriteAsync("这是我的第一个请求开始信息。");
                await next.Invoke(context);
                await context.Response.WriteAsync("这是我的第一个请求信息结束。");

            }));
            app.Use(next => new RequestDelegate(async context => {
                await context.Response.WriteAsync("这是我的第二个请求开始信息。");
                await next.Invoke(context);
                await context.Response.WriteAsync("这是我的第二个请求信息结束。");

            }));
            app.Use(next => new RequestDelegate(async context => {
                await context.Response.WriteAsync("这是我的第三个请求开始信息。"); 
                await context.Response.WriteAsync("这是我的第三个请求信息结束。");

            }));
            loggerFactory.AddConsole(minLevel:LogLevel.Information);
            app.UseRequestIP();//使用中间件
            app.UseStaticFiles();//使用静态文件中间件
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
