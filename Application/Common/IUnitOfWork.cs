
using Application.Interfaces.Repositories;

namespace Application.Common {
    public interface IUnitOfWork {
        public IBuyRepository BuyRepository { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IMedicineRepository MedicineRepository { get; set; }
        public ISalaryRepository SalaryRepository { get; set; }
        public ISellRepository SellRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
