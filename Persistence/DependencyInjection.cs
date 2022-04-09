
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence {
    public static class DependencyInjection {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("Pharmacy"));
                options.EnableDetailedErrors();
            });
        }
    }
}
