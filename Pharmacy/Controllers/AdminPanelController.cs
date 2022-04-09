using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class AdminPanelController : BaseController {
        public AdminPanelController(IApplicationServices applicationServices) : base(applicationServices) { }
        public async Task<IActionResult> Index () => await Task.FromResult(View());
        public async Task<IActionResult> Table () => await Task.FromResult(View());
        public async Task<IActionResult> Popup () =>  await Task.FromResult(View());
        public async Task<IActionResult> Login () =>  await Task.FromResult(View());
    }
}
