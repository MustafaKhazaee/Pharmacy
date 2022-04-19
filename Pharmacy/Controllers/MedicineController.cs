using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    public class MedicineController : BaseController {
        public MedicineController(IApplicationServices applicationServices) : base (applicationServices) { }
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Medicine> AddMedicine([FromForm] MedicineModel medicine) =>
           await services.MedicineService.SaveMedicine(medicine);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<DataTableResult<Medicine>> GetMedicineAsync(DataTableParams param) =>
            await services.MedicineService.GetMedicinePage(param);

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Medicine> DeleteMedicine(Guid id) => await services.MedicineService.SoftDeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetUpdateModal (Guid id) => 
            await Task.FromResult(PartialView("UpdateMedicine", await services.MedicineService.FindAsync(id)));

        [HttpGet]
        public async Task<SelectResult> GetList(string term) =>
            await services.MedicineService.GetList(term);

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<Medicine> UpdateMedicine([FromForm] MedicineModel medicine) =>
            await services.MedicineService.UpdateMedicine(medicine);
    }
}   
