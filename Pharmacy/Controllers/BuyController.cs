using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class BuyController : BaseController {
        public BuyController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Buy> AddBuy([FromForm] BuyModel buyModel) =>
            await services.BuyService.SaveBuy(buyModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<object>> GetBuyAsync(DataTableParams param) =>
            await services.BuyService.GetBuyPage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Buy> DeleteBuy(Guid id) => await services.BuyService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateBuy", await services.BuyService.FindAsync(id)));

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Buy> UpdateBuy([FromForm] BuyModel buyModel) =>
            await services.BuyService.UpdateBuy(buyModel);
    }
}   
