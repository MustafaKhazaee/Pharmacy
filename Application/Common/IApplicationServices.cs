
using Application.Interfaces.Services;

namespace Application.Common {
    public interface IApplicationServices {
        public IBuyService BuyService { get; set; }
        public ICompanyService CompanyService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public IEmployeeService EmployeeService { get; set; }
        public IMedicineService MedicineService { get; set; }
        public ISalaryService SalaryService { get; set; }
        public ISellService SellService { get; set; }
        public IUserService UserService { get; set; }
        public ITransactionService TransactionService { get; set; }
        public IPictureService PictureService { get; set; }
    }
}
