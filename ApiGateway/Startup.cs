using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using CacheManager.Core;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using System;
using Ocelot.JWTAuthorizePolicy;
namespace ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            services.AddOcelotJwtBearer(audienceConfig["Issuer"], audienceConfig["Audience"], audienceConfig["Secret"], "RyanBearer");
            services.AddOcelot(Configuration as ConfigurationRoot); //此处添加Ocelot服务
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseOcelot().Wait();//此处使用Ocelot服务
        }
    }
}
