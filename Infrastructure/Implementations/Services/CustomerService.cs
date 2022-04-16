
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class CustomerService : GenericService<Customer>, ICustomerService {
        public CustomerService(ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base(applicationDbContext, a) {
        }

        public async Task<DataTableResult<Customer>> GetCustomerPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Customer> list = await FindAllAsync(e => (e.FirstName.Contains(searchKey) || e.LastName.Contains(searchKey) ||
                    e.Mobile.Contains(searchKey) || searchKey == null) && !e.IsDeleted, start, length);
            return new DataTableResult<Customer> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }
    }
}
