using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class BaseController : Controller {
        protected readonly IApplicationServices services;
        public BaseController (IApplicationServices applicationServices) => services = applicationServices;
    }
}
