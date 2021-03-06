using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealTimeTodo.Web.Hubs;
using RealTimeTodo.Web.Services;

namespace RealTimeTodo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configure => 
            {
                configure.RootPath = "wwwroot";
            });

            services.AddSignalR();

            services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TodoHub>("/hubs/todo");
            });

            app.UseSpaStaticFiles();

            app.UseSpa(configure => 
            {
                configure.UseProxyToSpaDevelopmentServer("http://localhost:8080");
            });
        }
    }
}
