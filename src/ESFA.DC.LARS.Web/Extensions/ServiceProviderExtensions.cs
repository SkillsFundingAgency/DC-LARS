using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider ConfigureAutofac(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);
            var applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }
    }
}