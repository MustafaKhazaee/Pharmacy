
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository {
        public EmployeeRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
