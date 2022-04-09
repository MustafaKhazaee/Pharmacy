
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class CompanyService : GenericService<Company>, ICompanyService {
        public CompanyService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
