
using GdalNetCore.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GdalNetCore.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var gdalKit = new GdalKit();
            var info = gdalKit.GetGdalInfo();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Buildinfo: " + info.BuildInfo + Environment.NewLine);
                    await context.Response.WriteAsync("Releasename: " + info.ReleaseName + Environment.NewLine);
                    await context.Response.WriteAsync("Versionnumber: " + info.VersionNumber + Environment.NewLine);
                    await context.Response.WriteAsync("Number of drivers: " + info.Drivers.Count + Environment.NewLine);
                    await context.Response.WriteAsync("Drivers: " + string.Join(',', info.Drivers) + Environment.NewLine);

                });
            });
        }
    }
}
