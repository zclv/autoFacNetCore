using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.Dependency;
using NetCore.IService;
using Service.Service;

namespace autoFacNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                  .SetBasePath(env.ContentRootPath)
                  .AddJsonFile("appsettings.json").Build();
        } 

        public IContainer ApplicationContainer { get; private set; } 
        public IServiceProvider ConfigureServices(IServiceCollection services)
        { 
            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(UserContext), new UserContext(Configuration.GetConnectionString("DefaultConnection"))));
            var builder = new ContainerBuilder();//实例化 AutoFac  容器   
              
            var assemblys = Assembly.Load("Service");//Service是继承接口的实现方法类库名称
            var baseType = typeof(IDependency);//IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
            builder.RegisterAssemblyTypes(assemblys)
             .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //loggerFactory.AddConsole(minLevel:LogLevel.Information);
            //var logger = loggerFactory.CreateLogger("Middleware");
            //app.Use(async(context,next)=>{
            //    logger.LogInformation("Handing request");
            //    await next.Invoke();
            //    logger.LogInformation("Finished handing request");
            //});
            //app.Run(async context=> {
            //    await context.Response.WriteAsync("Hello from MiddleWare");
            //});
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
