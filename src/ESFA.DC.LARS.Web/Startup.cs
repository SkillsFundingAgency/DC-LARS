using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.LARS.Web.Configuration;
using ESFA.DC.LARS.Web.Extensions;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Modules;
using Flurl.Http;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESFA.DC.LARS.Web
{
    public class Startup
    {
        private IContainer _applicationContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var insightOptions = new ApplicationInsightsServiceOptions
            {
                // Disables adaptive sampling.
                EnableAdaptiveSampling = false,

                // Disables QuickPulse (Live Metrics stream).
                EnableQuickPulseMetricStream = false
            };
            services.AddApplicationInsightsTelemetry(insightOptions);

            services.AddSingleton<Models.IAppVersionService, AppVersionService>();

            FlurlHttp.Configure(settings =>
            {
                settings.HttpClientFactory = new PollyHttpClientFactory();
            });

            return ConfigureAutofac(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<WebServicesModule>();

            containerBuilder.Register(c =>
                    Configuration.GetConfigSection<ApiSettings>())
                .As<IApiSettings>().SingleInstance();

            containerBuilder.Populate(services);
            _applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(_applicationContainer);
        }
    }
}
