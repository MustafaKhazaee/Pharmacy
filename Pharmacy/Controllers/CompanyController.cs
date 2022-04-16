using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class CompanyController : BaseController {
        public CompanyController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Company> AddCompany([FromForm] Company Company) =>
           (Company) await services.CompanyService.AddAsync(Company);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<Company>> GetCompanyAsync(DataTableParams param) =>
            await services.CompanyService.GetCompanyPage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Company> DeleteCompany(Guid id) => await services.CompanyService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateCompany", await services.CompanyService.FindAsync(id)));

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Company> UpdateCompany([FromForm] Company Company) =>
            (Company) await services.CompanyService.UpdateAsync(Company);
    }
}   
