using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class EmployeeController : BaseController {
        public EmployeeController (IApplicationServices applicationServices) : base (applicationServices) { }

        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Employee> AddEmployee([FromForm] EmployeeModel employee) =>
            await services.EmployeeService.SaveEmployee(employee);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<Employee>> GetEmployeesAsync(DataTableParams param) =>
                await services.EmployeeService.GetEmployeePage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Employee> DeleteEmployee(Guid id) => await services.EmployeeService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateEmployee", await services.EmployeeService.FindAsync(id)));

        [HttpGet]
        public async Task<SelectResult> GetList(string term) =>
            await services.EmployeeService.GetList(term);

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Employee> UpdateEmployee([FromForm] EmployeeModel employee) =>
            await services.EmployeeService.UpdateEmployee(employee);
    }
}   
