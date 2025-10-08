

using Inventory.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Persistence.DependencyInjection
{
    public static  class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistenceDependencyInjection(this IServiceCollection services ,IConfiguration configuration)
        {


            #region DbContext
            services.AddDbContext<InventoryDbContext>(options =>
            {
                var con = configuration.GetConnectionString("ConnectionString");
                options.UseSqlServer(con);
            });
            #endregion
            return services;
        }

    }
}
