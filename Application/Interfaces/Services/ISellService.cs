
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface ISellService : IGenericService<Sell> {
        public Task<Sell> SaveSell(SellModel sellModel);
        public Task<List<Object>> SaveSellCart (List<SellModel> sells);
        public Task<Sell> UpdateSell(SellModel sellModel);
        public Task<Sell> SoftDeleteSellAsync(Guid Id);
        public Task<DataTableResult<object>> GetSellPage(DataTableParams param);
        public Task<DataTableResult<object>> GetSellReport(SellReportModel sellReportModel);
    }
}
