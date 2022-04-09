
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class CustomerService : GenericService<Customer>, ICustomerService {
        public CustomerService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
