using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CoreProject.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using CoreProject.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CoreProject
{
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
            // Add framework services.
            services.AddMvc();

            // Add db Connection
            var connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<WarrantyContext>(options => options.UseSqlServer(connection));

            // Add Identity
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(IdentityConfigs.GetApiResources())
                .AddInMemoryClients(IdentityConfigs.GetClients())
                .AddTestUsers(IdentityConfigs.GetTestUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Microsoft.Extensions.Logging
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            // app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // logging - filtered
            // loggerFactory.AddConsole((str, level) => { return level >= LogLevel.Trace; });
            loggerFactory.AddDebug();
            var log = loggerFactory.CreateLogger("Wu");
            log.LogWarning("Haaaaa", "a", "aaaa");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // 使用IdentityServer
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
