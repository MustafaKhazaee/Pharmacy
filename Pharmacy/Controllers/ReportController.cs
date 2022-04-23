using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class ReportController : BaseController {
        public ReportController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Sell () => await Task.FromResult(View());
        public async Task<IActionResult> Buy () => await Task.FromResult(View());
        public async Task<IActionResult> Salary () => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<object>> BuyReport (BuyReportModel buyReportModel) =>
            await services.BuyService.GetBuyReport(buyReportModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<object>> SellReport (SellReportModel sellReportModel) =>
            await services.SellService.GetSellReport(sellReportModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<object>> SalaryReport (SalaryReportModel salaryReportModel) =>
            await services.SalaryService.GetSalaryReport(salaryReportModel);
    }
}   
