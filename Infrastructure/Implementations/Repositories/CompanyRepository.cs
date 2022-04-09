
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository {
        public CompanyRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
