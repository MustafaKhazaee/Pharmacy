
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface ICustomerService : IGenericService<Customer> {
        public Task<DataTableResult<Customer>> GetCustomerPage(DataTableParams param);
        public Task<SelectResult> GetList(string key);
    }
}
