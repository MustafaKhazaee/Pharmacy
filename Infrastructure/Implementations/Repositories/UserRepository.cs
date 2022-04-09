
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class UserRepository : GenericRepository<User>, IUserRepository {
        public UserRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
