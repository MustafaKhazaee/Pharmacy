
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class SalaryService : GenericService<Salary>, ISalaryService {
        public SalaryService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
