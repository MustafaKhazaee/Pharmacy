
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class SellService : GenericService<Sell>, ISellService {
        public SellService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
