using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class SalaryController : BaseController {
        public SalaryController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Salary> AddSalary([FromForm] SalaryModel salaryModel) =>
            await services.SalaryService.SaveSalary(salaryModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<Salary>> GetSalaryAsync(DataTableParams param) =>
            await services.SalaryService.GetSalaryPage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Salary> DeleteSalary(Guid id) => await services.SalaryService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateSalary", await services.SalaryService.FindAsync(id)));

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Salary> UpdateSalary([FromForm] Salary salary) =>
            (Salary) await services.SalaryService.UpdateAsync(salary);
    }
}   
