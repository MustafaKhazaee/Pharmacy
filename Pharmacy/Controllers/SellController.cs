using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class SellController : BaseController {
        public SellController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Sell> AddSell([FromForm] SellModel sellModel) =>
            await services.SellService.SaveSell(sellModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<object>> GetSellAsync(DataTableParams param) =>
            await services.SellService.GetSellPage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Sell> DeleteSell(Guid id) => await services.SellService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> PrintBill(Guid id) => 
            await Task.FromResult(PartialView("SellBillPrint", await services.SellService.FindAsync(id)));

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Sell> UpdateSell([FromForm] SellModel sellModel) =>
            await services.SellService.UpdateSell(sellModel);
    }
}   
