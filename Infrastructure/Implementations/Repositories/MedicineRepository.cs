
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository {
        public MedicineRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
