
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class MedicineService : GenericService<Medicine>, IMedicineService {
        public MedicineService (ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor) : base (applicationDbContext, httpContextAccessor) { }
        public async Task<DataTableResult<Medicine>> GetMedicinePage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Medicine> list = await FindAllAsync(e => (e.Name.Contains(searchKey) ||
                e.Description.Contains(searchKey) || searchKey == null) && !e.IsDeleted, start, length);
            return new DataTableResult<Medicine> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }

        public async Task<Medicine> SaveMedicine(MedicineModel medicineModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(medicineModel.Photo, 100, 100, "medicine");
            Medicine medicine = ViewModelToEntity(medicineModel, ph);
            return (Medicine)await AddAsync(medicine);
        }

        public async Task<Medicine> UpdateMedicine(MedicineModel medicineModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(medicineModel.Photo, 100, 100, "medicine");
            Medicine medicine = await FindAsync(medicineModel.id);
            medicine.Name = medicineModel.Name;
            medicine.SellProfitPercent = medicineModel.SellProfitPercent;
            medicine.BuyPrice = medicineModel.BuyPrice;
            medicine.ExpirationDate = medicineModel.ExpirationDate;
            medicine.ManufacturingCompany = medicineModel.ManufacturingCompany;
            medicine.Description = medicineModel.Description;
            medicine.Type = medicineModel.Type;
            medicine.Count = medicineModel.Count;
            medicine.ManufacturingDate = medicineModel.ManufacturingDate;
            medicine.Category = medicineModel.Category;
            if (ph != null) {
                medicine.Photo = ph[0];
                medicine.PhotoResized = ph[1];
            }
            return (Medicine)await UpdateAsync(medicine);
        }

        private Medicine ViewModelToEntity(MedicineModel medicineModel, string[] ph) => new Medicine {
            Name = medicineModel.Name,
            SellProfitPercent = medicineModel.SellProfitPercent,
            BuyPrice = medicineModel.BuyPrice,
            ExpirationDate = medicineModel.ExpirationDate,
            ManufacturingDate = medicineModel.ManufacturingDate,
            Description = medicineModel.Description,
            Type = medicineModel.Type,
            Count = medicineModel.Count,
            ManufacturingCompany = medicineModel.ManufacturingCompany,
            Category = medicineModel.Category,
            Photo = ph != null ? ph[0] : null,
            PhotoResized = ph != null ? ph[1] : null
        };
    }
}
