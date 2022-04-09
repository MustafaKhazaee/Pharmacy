using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class HomeController : BaseController {
        public HomeController(IApplicationServices applicationServices) : base (applicationServices) { }
        public IActionResult Index() {
            return View();
        }
    }
}
