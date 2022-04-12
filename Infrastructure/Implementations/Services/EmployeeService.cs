
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
                DateOfBirth = employeeModel.DateOfBirth,
                Photo = ph != null ? ph[0] : null,
                PhotoResized = ph != null ? ph[1] : null
            };
            return (Employee) await AddAsync(employee);
        }
        
        public async Task<Employee> UpdateEmployee(EmployeeModel employeeModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(employeeModel.Photo, 100, 100);
            Employee employee = new Employee {
                Id = (Guid) employeeModel.id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Address = employeeModel.Address,
                Mobile = employeeModel.Mobile,
                Email = employeeModel.Email,
                DateOfBirth = employeeModel.DateOfBirth,
                Photo = ph != null ? ph[0] : null,
                PhotoResized = ph != null ? ph[1] : null
            };
            return (Employee) await UpdateAsync(employee);
        }

        public async Task<DataTableResult<Employee>> GetEmployeePage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Employee> list = await FindAllAsync(e => (e.FirstName.Contains(searchKey) || e.LastName.Contains(searchKey) ||
                    e.Mobile.Contains(searchKey) || searchKey == null) && !e.IsDeleted, start, length);
            return new DataTableResult<Employee> {
                Data = list,
                RecordsFiltered = list.Count(),
                RecordsTotal = all,
                Draw = param.Draw
            };
        }
    }
}
