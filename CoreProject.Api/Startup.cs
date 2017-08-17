using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreProject.Api
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

            // add cors policy , adaption with configuration
            services.AddCors(options =>
            {
                foreach (var a in Configuration.GetSection("Cors").GetChildren())
                {
                    options.AddPolicy(a.Key, policy =>
                    {
                        policy.WithOrigins(a.Value)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // 配置identityServer授权
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority="http://localhost:8000",
                AllowedScopes = {"UserApi"},
                RequireHttpsMetadata=false
            });

            // 跨域访问
            foreach (var a in Configuration.GetSection("Cors").GetChildren())
            {
                app.UseCors(a.Key);
            }
            app.UseMvc();
        }
    }
}
