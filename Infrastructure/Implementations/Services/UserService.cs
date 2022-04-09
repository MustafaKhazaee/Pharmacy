
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class UserService : GenericService<User>, IUserService {
        public UserService(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {
        }
    }
}
