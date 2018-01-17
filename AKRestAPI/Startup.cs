using AKRestAPI.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AKRestAPI
{

    // Just adding a helper method here
    public static class Util
    {
        public static String GetEnv(String key)
        {
            // Most shells need double quotes around string values. Docker doesn't play well with quotes. This seems like the easiest way to deal for now.
            // Replace all double quotes with nothing
            return Environment.GetEnvironmentVariable(key).Replace("\"", "");
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS support
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc();

            // Dependency injection
            var soap_endpoint = AKRestAPI.Util.GetEnv("SoapEndpoint");
            services.AddSingleton<PeopleContext>(new PeopleContext(soap_endpoint));
            services.AddScoped<IPeopleRepository, PeopleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "sql",
                        template: "{controller=Sql}/{action=Index}"
                    );
                     routes.MapRoute(
                        name: "aries",
                        template: "{controller=Aries}/{action=Index}"
                    );
                    routes.MapRoute(
                        name: "eis",
                        template: "{controller=EIS}/{action=Index}"
                    );
                     routes.MapRoute(
                        name: "btaries",
                        template: "{controller=BizTalk}/{action=Aries}"
                    );
                     routes.MapRoute(
                        name: "bteis",
                        template: "{controller=BizTalk}/{action=EIS}"
                    );
                }  
            );

        }
    }
}
