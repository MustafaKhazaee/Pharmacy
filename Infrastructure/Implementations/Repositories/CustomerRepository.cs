
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository {
        public CustomerRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
