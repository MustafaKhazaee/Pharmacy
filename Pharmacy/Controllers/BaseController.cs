using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    [Authorize]
    public class BaseController : Controller {
        protected readonly IApplicationServices services;
        public BaseController (IApplicationServices applicationServices) => services = applicationServices;
    }
}
            