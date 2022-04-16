
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class TransactionService : GenericService<Transaction>, ITransactionService {
        public TransactionService(ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base(applicationDbContext, a) {
        }
    }
}
