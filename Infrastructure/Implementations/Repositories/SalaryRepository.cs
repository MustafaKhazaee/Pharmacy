
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository {
        public SalaryRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
