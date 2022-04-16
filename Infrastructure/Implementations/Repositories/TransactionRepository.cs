
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Repositories {
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository {
        public TransactionRepository(ApplicationDbContext _context) : base(_context) {
        }
    }
}
