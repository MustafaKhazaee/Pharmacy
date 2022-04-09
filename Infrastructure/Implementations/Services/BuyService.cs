
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class BuyService : GenericService<Buy>, IBuyService {
        public BuyService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
