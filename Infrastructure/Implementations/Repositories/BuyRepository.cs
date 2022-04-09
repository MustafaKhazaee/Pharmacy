
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class BuyRepository : GenericRepository<Buy>, IBuyRepository {
        public BuyRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
