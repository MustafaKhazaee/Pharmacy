
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class BuyService : GenericService<Buy>, IBuyService {
        public BuyService(ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base(applicationDbContext, a) {
        }

        public async Task<DataTableResult<object>> GetBuyPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<object> list = await Task.FromResult(context.Buys.Where(e => (e.Medicine.Name.Contains(searchKey)
                || e.Remarks.Contains(searchKey) || searchKey == null) && !e.IsDeleted).Select(s => new {
                    id = s.Id,
                    name = $"{s.Medicine.Name} ({s.Medicine.Type} {s.Medicine.Category})",
                    companyName = $"{s.Company.Name}",
                    date = s.BuyDate,
                    count = s.Count,
                    totalPrice = s.TotalPrice,
                    totalPaid = s.TotalPaid,
                    buyBill = s.BuyBill,
                    remarks = s.Remarks
                }).Skip(start).Take(length).ToList());
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }

        public async Task<Buy> SaveBuy(BuyModel buyModel) {
            PictureService pictureService = new PictureService();
            string ph = pictureService.SavePicture(buyModel.BuyBill, "buyBills");
            Buy buy = ViewModelToEntity(buyModel, ph);
            return (Buy) await AddAsync(buy);
        }

        public async Task<Buy> UpdateBuy(BuyModel buyModel) {
            PictureService pictureService = new PictureService();
            string ph = pictureService.SavePicture(buyModel.BuyBill, "buyBills");
            Buy buy = await FindAsync(buyModel.Id);
            buy.MedicineId = buyModel.MedicineId;
            buy.CompanyId = buyModel.CompanyId;
            buy.BuyDate = buyModel.BuyDate;
            buy.Count = buyModel.Count;
            buy.TotalPrice = buyModel.TotalPrice;
            buy.TotalPaid = buyModel.TotalPaid;
            buy.Remarks = buyModel.Remarks;
            if (ph != null) {
                buy.BuyBill = ph;
            }
            return (Buy) await UpdateAsync(buy);
        }

        private Buy ViewModelToEntity(BuyModel buyModel, string ph) => new Buy {
            MedicineId = buyModel.MedicineId,
            CompanyId = buyModel.CompanyId,
            BuyDate = buyModel.BuyDate,
            Count = buyModel.Count,
            TotalPrice = buyModel.TotalPrice,
            TotalPaid = buyModel.TotalPaid,
            Remarks = buyModel.Remarks,
            BuyBill = ph
        };
    }
}
