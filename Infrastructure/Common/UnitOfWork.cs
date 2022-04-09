
using Application.Common;
using Application.Interfaces.Repositories;
using Infrastructure.Implementations.Repositories;
using Persistence;

namespace Infrastructure.Common {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _context;
        public UnitOfWork (ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
            BuyRepository = new BuyRepository(_context);
            CompanyRepository = new CompanyRepository(_context);
            CustomerRepository = new CustomerRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
            MedicineRepository = new MedicineRepository(_context);
            SalaryRepository = new SalaryRepository(_context);
            SellRepository = new SellRepository(_context);
            UserRepository = new UserRepository(_context);
        }

        public IBuyRepository BuyRepository { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IMedicineRepository MedicineRepository { get; set; }
        public ISalaryRepository SalaryRepository { get; set; }
        public ISellRepository SellRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public async Task<int> CompleteAsync (CancellationToken cancellationToken) => await _context.SaveChangesAsync();
    }
}
