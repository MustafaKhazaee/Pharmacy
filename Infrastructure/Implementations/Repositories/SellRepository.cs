
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class SellRepository : GenericRepository<Sell>, ISellRepository {
        public SellRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
