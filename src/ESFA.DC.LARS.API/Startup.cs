﻿using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.LARS.API.Configuration;
using ESFA.DC.LARS.API.CustomFilters;
using ESFA.DC.LARS.API.Extensions;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Modules;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ESFA.DC.LARS.API
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
            services.AddMvc(options =>
                    {
                        options.Filters.Add(typeof(TelemetryActionFilter));
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApplicationInsightsTelemetry();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learning Aim Reference Service API", Version = "v1" });
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

            app.ConfigureExceptionHandler();

            app.UseRouting();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning Aim Reference Service API");
            });

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                             name: "areaRoute",
                             pattern: "{area:exists}/{controller}/{action}/{id?}",
                             defaults: new { action = "Index" });
            });
        }

        private IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);

            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<ApiModule>();
            containerBuilder.RegisterModule(new AzureModule
            {
                AzureSettings = Configuration.GetConfigSection<AzureSettings>()
            });

            _applicationContainer = containerBuilder.Build();

            _applicationContainer.Resolve<ILearningAimService>();

            return new AutofacServiceProvider(_applicationContainer);
        }
    }
}
