
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions {
    public static class AuthenticationExtension {
        public static void AddCookieAuthentication (this IServiceCollection services) {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/AdminPanel/AccessDenied";
                options.LoginPath = "/AdminPanel/LoginPage";
                options.LogoutPath = "/AdminPanel/LoginPage";
            });
        }
    }
}
