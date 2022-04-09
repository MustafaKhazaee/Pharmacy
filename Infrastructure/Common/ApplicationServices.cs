
using Application.Common;
using Application.Interfaces.Services;
using Infrastructure.Implementations.Services;
using Persistence;

namespace Infrastructure.Common {
    public class ApplicationServices : IApplicationServices {
        public ApplicationServices (ApplicationDbContext context) {
            BuyService = new BuyService (context);
            CompanyService = new CompanyService(context);
            CustomerService = new CustomerService(context);
            EmployeeService = new EmployeeService(context);
            MedicineService = new MedicineService(context);
            SalaryService = new SalaryService(context);
            SellService = new SellService(context);
            UserService = new UserService(context);
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
    }
}
