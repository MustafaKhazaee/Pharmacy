
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class EmployeeService : GenericService<Employee>, IEmployeeService {
        public EmployeeService (ApplicationDbContext applicationDbContext) : base (applicationDbContext) {}

        public async Task<Employee> SaveEmployee(EmployeeModel employeeModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(employeeModel.Photo, 100, 100);
            Employee employee = new Employee {
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Address = employeeModel.Address,
                Mobile = employeeModel.Mobile,
                Email = employeeModel.Email,
                Photo = ph != null ? ph[0] : null,
                PhotoResized = ph != null ? ph[1] : null
            };
            return (Employee) await AddAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetEmployeePage(string firstName, string lastName, string email, string mobile, int pageIndex, int pageSize) =>
            await FindAllAsync(e => e.FirstName.Contains(firstName) || e.LastName.Contains(lastName) ||
                e.Email.Contains(email) || e.Mobile.Contains(mobile), pageIndex, pageSize);
    }
}
