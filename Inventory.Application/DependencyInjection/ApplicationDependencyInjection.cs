using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
