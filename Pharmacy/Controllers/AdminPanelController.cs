using Application.Common;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class AdminPanelController : BaseController {
        public AdminPanelController(IApplicationServices applicationServices) : base(applicationServices) { }
        public async Task<IActionResult> Index () => await Task.FromResult(View());
        public async Task<string> GetUserName() => await Task.FromResult(HttpContext.User.Identity.Name);
        public async Task<IActionResult> Table () => await Task.FromResult(View());
        public async Task<IActionResult> Popup () =>  await Task.FromResult(View());
        [AllowAnonymous]
        public async Task<IActionResult> LoginPage () =>  await Task.FromResult(View());
        [AllowAnonymous]
        public async Task<bool> Login ([FromForm] LoginModel loginModel) => await services.UserService.LoginUser(loginModel);
        public async Task<bool> Logout() => await services.UserService.LogoutUser();

    }
}
