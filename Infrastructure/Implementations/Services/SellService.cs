
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class SellService : GenericService<Sell>, ISellService {
        public SellService(ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base(applicationDbContext, a) { }
        public async Task<DataTableResult<object>> GetSellPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<object> list = await Task.FromResult(context.Sells.Where(e => (e.Medicine.Name.Contains(searchKey)
                || e.Remarks.Contains(searchKey) || searchKey == null) && !e.IsDeleted).Select(s => new {
                    id = s.Id,
                    name = $"{s.Medicine.Name} ({s.Medicine.Type} {s.Medicine.Category})",
                    customerName = $"{s.Customer.FirstName} {s.Customer.LastName} ({s.Customer.Mobile})",
                    date = s.SellDate,
                    count = s.Count,
                    totalPrice = s.TotalPrice,
                    remarks = s.Remarks
                }).Skip(start).Take(length).ToList());
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }

        public async Task<DataTableResult<object>> GetSellReport(SellReportModel sellReportModel) {
            DateTime? fromDate = sellReportModel.fromDate, toDate = sellReportModel.toDate;
            Guid? comId = sellReportModel.customerId, medId = sellReportModel.medicineId;
            int count = await CountAsync();
            List<object> list = context.Sells.Where(b => (comId == null || comId == b.CustomerId) &&
                            (medId == null || medId == b.MedicineId) &&
                            (fromDate == null || fromDate <= b.SellDate) &&
                            (toDate == null || toDate >= b.SellDate)
                 )
                .GroupBy(b => new {
                    cFirstName = b.Customer.FirstName,
                    cLastName = b.Customer.LastName,
                    medName = b.Medicine.Name,
                    medType = b.Medicine.Type,
                    medCat = b.Medicine.Category
                })
               .Select(b => new {
                   totalPrice = b.Sum(b => b.TotalPrice),
                   customer = $"{b.Key.cFirstName} {b.Key.cLastName}",
                   medicine = $"{b.Key.medName} ({b.Key.medType} {b.Key.medCat})"
               }).ToList<object>();
            int totalPrice = 0;
            foreach (dynamic i in list) {
                totalPrice += i.totalPrice;
            }
            list.Add(new {
                customer = "",
                medicine = "مجموعه",
                totalPrice = totalPrice
            });
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = list.Count(),
                RecordsTotal = list.Count(),
                Draw = 1
            };
        }
        public async Task<Sell> SaveSell(SellModel sellModel) {
            Sell sell = ViewModelToEntity(sellModel);
            Medicine m = await context.Medicines.FindAsync(sellModel.MedicineId);
            if (m.Count < sell.Count) {
                sell.Remarks = "Not enough medicine";
                return sell;
            }
            m.Count -= sell.Count;
            return (Sell) await AddAsync(sell);
        }

        public async Task<List<Object>> SaveSellCart(List<SellModel> sells) {
            List<Sell> sellList = new List<Sell>();
            List<Guid> medicineIds = new List<Guid>();
            List<Object> result = new List<object>();
            sells.ForEach(s => {
                sellList.Add(ViewModelToEntity(s));
                medicineIds.Add(s.MedicineId);
            });
            List<Medicine> list = await Task.FromResult(context.Medicines.Where(m => medicineIds.Contains(m.Id)).ToList());
            foreach(Medicine i in list) {
                SellModel s = sells.First(s => s.MedicineId == i.Id);
                if (i.Count < s.Count) {
                    result.Add(new {
                        medicine = "Not enough medicine"
                    });
                    return result;
                } else if (i.RemainingUsableDays < 10) {
                    int t = i.RemainingUsableDays > 0 ? i.RemainingUsableDays : Math.Abs(i.RemainingUsableDays);
                    result.Add(new {
                        medicine = "Medicine has expired",
                        message = i.RemainingUsableDays > 0 ? $"{i.Name} expires in {t} days" : $"{i.Name} has expired {t} days ago"
                    });
                    return result;
                }
            };
            foreach (Medicine i in list) {
                SellModel s = sells.First(s => s.MedicineId == i.Id);
                i.Count -= s.Count;
            }
            await context.Sells.AddRangeAsync(sellList);
            await context.SaveChangesAsync();
            sells.ForEach(i => {
                Medicine m = list.First(m => m.Id == i.MedicineId);
                result.Add(new {
                    medicine = m.Name,
                    price = m.BuyPrice + m.BuyPrice / 100 * m.SellProfitPercent,
                    count = i.Count,
                    totalPrice = i.TotalPrice
                });
            });
            return result;
        }

        public async Task<Sell> SoftDeleteSellAsync(Guid Id) {
            Sell sell = await FindAsync(Id);
            Medicine m = await context.Medicines.FindAsync(sell.MedicineId);
            m.Count += sell.Count;
            await SoftDeleteAsync(Id);
            return sell;
        }

        public async Task<Sell> UpdateSell(SellModel sellModel) {
            Sell sell = await FindAsync(sellModel.Id);
            Medicine m = await context.Medicines.FindAsync(sellModel.MedicineId);
            if (sell.Count + m.Count < sellModel.Count)
                return sell;
            m.Count += sell.Count - sellModel.Count;
            sell.MedicineId = sellModel.MedicineId;
            sell.CustomerId = sellModel.CustomerId;
            sell.SellDate = sellModel.SellDate;
            sell.Count = sellModel.Count;
            sell.TotalPrice = sellModel.TotalPrice;
            sell.Remarks = sellModel.Remarks;
            return (Sell)await UpdateAsync(sell);
        }

        private Sell ViewModelToEntity(SellModel sellModel) => new Sell {
            MedicineId = sellModel.MedicineId,
            CustomerId = sellModel.CustomerId,
            SellDate = sellModel.SellDate,
            Count = sellModel.Count,
            TotalPrice = sellModel.TotalPrice,
            Remarks = sellModel.Remarks,
        };
    }
}
