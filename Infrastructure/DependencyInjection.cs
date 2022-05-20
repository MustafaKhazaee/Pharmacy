using Application.Common;
using Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure {
    public static class DependencyInjection {
        public static void AddInfrastructure(this IServiceCollection services) {
            services.AddScoped<IApplicationServices, ApplicationServices>();
        }
    }
}
