
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class MedicineService : GenericService<Medicine>, IMedicineService {
        public MedicineService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
