using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class EmployeeController : BaseController {
        public EmployeeController (IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        public async Task<object> AddEmployee([FromForm] EmployeeModel employee) =>
            await services.EmployeeService.SaveEmployee(employee);

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int start, int draw) {

            var a = await services.EmployeeService.GetAllAsync();
            return a;
        }
            
    }
}   
