
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<DataTableResult<object>> GetBuyReport(BuyReportModel buyReportModel) {
            DateTime? fromDate = buyReportModel.fromDate, toDate = buyReportModel.toDate;
            Guid? comId = buyReportModel.companyId, medId = buyReportModel.medicineId;
            int count = await CountAsync();
            List<object> list = context.Buys.Where(b => (comId == null || comId == b.CompanyId) &&
                            (medId == null || medId == b.MedicineId) &&
                            (fromDate == null || fromDate <= b.BuyDate) &&
                            (toDate == null || toDate >= b.BuyDate)
                 )
                .GroupBy(b => new {
                    comName = b.Company.Name,
                    medName = b.Medicine.Name,
                    medType = b.Medicine.Type,
                    medCat = b.Medicine.Category
                })
               .Select(b => new {
                   totalPrice = b.Sum(b => b.TotalPrice),
                   totalPaid = b.Sum(b => b.TotalPaid),
                   totalRemain = b.Sum(b => b.TotalPrice) - b.Sum(b => b.TotalPaid),
                   company = b.Key.comName,
                   medicine = $"{b.Key.medName} ({b.Key.medType} {b.Key.medCat})"
               }).ToList<object>();
            int totalPrice = 0, totalPaid = 0, totalRemain = 0;
            foreach (dynamic i in list) {
                totalPrice += i.totalPrice;
                totalPaid += i.totalPaid;
                totalRemain += i.totalRemain;
            }
            list.Add(new {
                company = "",
                medicine = "مجموعه",
                totalPrice = totalPrice,
                totalPaid = totalPaid,
                totalRemain = totalRemain
            });
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = list.Count(),
                RecordsTotal = list.Count(),
                Draw = 1
            };
        }

        public async Task<Buy> SaveBuy(BuyModel buyModel) {
            PictureService pictureService = new PictureService();
            string ph = pictureService.SavePicture(buyModel.BuyBill, "buyBills");
            Buy buy = ViewModelToEntity(buyModel, ph);
            Medicine m = await context.Medicines.FindAsync(buyModel.MedicineId);
            m.Count += buyModel.Count;
            return (Buy) await AddAsync(buy);
        }

        public async Task<Buy> SoftDeleteBuyAsync(Guid Id) {
            Buy buy = await FindAsync(Id);
            Medicine m = await context.Medicines.FindAsync(buy.MedicineId);
            m.Count -= buy.Count;
            await SoftDeleteAsync(Id);
            return buy;
        }

        public async Task<Buy> UpdateBuy(BuyModel buyModel) {
            PictureService pictureService = new PictureService();
            string ph = pictureService.SavePicture(buyModel.BuyBill, "buyBills");
            Buy buy = await FindAsync(buyModel.Id);
            Medicine m = await context.Medicines.FindAsync(buyModel.MedicineId);
            if (m.Count - buy.Count + buyModel.Count < 0)
                return buy;
            m.Count += -buy.Count + buyModel.Count;
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
