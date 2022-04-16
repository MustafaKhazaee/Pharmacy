
using Application.Common;
using Application.Interfaces.Services;
using Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Common {
    public class ApplicationServices : IApplicationServices {
        public ApplicationServices (ApplicationDbContext context, IHttpContextAccessor a) {
            BuyService = new BuyService (context, a);
            CompanyService = new CompanyService(context, a);
            CustomerService = new CustomerService(context, a);
            EmployeeService = new EmployeeService(context, a);
            MedicineService = new MedicineService(context, a);
            SalaryService = new SalaryService(context, a);
            SellService = new SellService(context, a);
            UserService = new UserService(context, a);
            TransactionService = new TransactionService(context, a);
            PictureService = new PictureService();
        }

        public IBuyService BuyService { get; set; }
        public ICompanyService CompanyService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public IEmployeeService EmployeeService { get; set; }
        public IMedicineService MedicineService { get; set; }
        public ISalaryService SalaryService { get; set; }
        public ISellService SellService { get; set; }
        public IUserService UserService { get; set; }
        public IPictureService PictureService { get; set; }
        public ITransactionService TransactionService { get; set; }
    }
}
