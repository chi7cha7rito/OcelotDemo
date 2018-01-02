using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.JWTAuthorizePolicy;
using App.Metrics;
using System;

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

            #region 注入metrics
            //var metrics = AppMetrics.CreateDefaultBuilder()
            //        .Configuration.Configure(
            //        options =>
            //        {
            //            options.AddAppTag("RyanApp");
            //            options.AddEnvTag("stage");
            //        })
            //        .Report.ToInfluxDb(
            //        options =>
            //        {
            //            options.InfluxDb.BaseUri = new Uri("http://localhost:8086");
            //            options.InfluxDb.Database = "MetcicsDB";
            //            options.InfluxDb.UserName = "root";
            //            options.InfluxDb.Password = "1qaz!QAZ";
            //            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            //            options.HttpPolicy.FailuresBeforeBackoff = 5;
            //            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
            //            options.FlushInterval = TimeSpan.FromSeconds(5);
            //        }).Build();
            //services.AddMetrics(metrics);
            //services.AddMetricsReportScheduler();
            //services.AddMetricsTrackingMiddleware();
            //services.AddMetricsEndpoints();
            #endregion

            var audienceConfig = Configuration.GetSection("Audience");
            services.AddOcelotJwtBearer(audienceConfig["Issuer"], audienceConfig["Audience"], audienceConfig["Secret"], "RyanBearer");
            services.AddOcelot(Configuration as ConfigurationRoot); //此处添加Ocelot服务
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //app.UseMetricsAllMiddleware();
            //app.UseMetricsAllEndpoints();
            app.UseOcelot().Wait();//此处使用Ocelot服务
        }
    }
}
