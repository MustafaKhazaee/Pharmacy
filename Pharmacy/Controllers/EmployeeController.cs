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

        [HttpPost]
        public async Task<DataTableResult<Employee>> GetEmployeesAsync(DataTableParams param) {
            var a = await services.EmployeeService.GetEmployeePage(param);
            return a;
        }
        [HttpDelete]
        public async Task<Employee> DeleteEmployee(Guid id) => await services.EmployeeService.SoftDeleteAsync(id, "A");
    }
}   
