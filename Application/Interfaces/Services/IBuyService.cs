
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface IBuyService : IGenericService<Buy> {
        public Task<Buy> SaveBuy(BuyModel buyModel);
        public Task<Buy> UpdateBuy(BuyModel buyModel);
        public Task<DataTableResult<object>> GetBuyPage(DataTableParams param);
        public Task<DataTableResult<object>> GetBuyReport(BuyReportModel buyReportModel);
    }
}
