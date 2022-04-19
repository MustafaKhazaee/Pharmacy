
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

        public async Task<Sell> SaveSell(SellModel sellModel) {
            Sell sell = ViewModelToEntity(sellModel);
            return (Sell) await AddAsync(sell);
        }

        public async Task<Sell> UpdateSell(SellModel sellModel) {
            Sell sell = await FindAsync(sellModel.Id);
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
