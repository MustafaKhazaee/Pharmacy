
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface ICompanyService : IGenericService<Company> {
        public Task<DataTableResult<Company>> GetCompanyPage(DataTableParams param);
        public Task<SelectResult> GetList(string key);
    }
}
