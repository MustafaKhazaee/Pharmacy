using Application.Extensions;
using Infrastructure;
using Persistence;
namespace WebUI {
    public class Startup {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services) {
            services.AddPersistence(Configuration);
            services.AddControllersWithViews();
            services.AddInfrastructure();
            services.AddCookieAuthentication();
            services.AddHttpContextAccessor();
            services.AddAntiforgery(a => a.HeaderName = "pharmacyapp-anti-forgery-token");
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Index}/{id?}");
            });
        }
    }
}