using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class CustomerController : BaseController {
        public CustomerController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Customer> AddCustomer([FromForm] Customer customer) =>
           (Customer) await services.CustomerService.AddAsync(customer);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<Customer>> GetCustomerAsync(DataTableParams param) =>
            await services.CustomerService.GetCustomerPage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Customer> DeleteCustomer(Guid id) => await services.CustomerService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateCustomer", await services.CustomerService.FindAsync(id)));

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Customer> UpdateCustomer([FromForm] Customer customer) =>
            (Customer) await services.CustomerService.UpdateAsync(customer);

        [HttpGet]
        public async Task<SelectResult> GetList(string term) =>
            await services.CustomerService.GetList(term);
    }
}   
