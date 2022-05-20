
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Implementations.Services {
    public class EmployeeService : GenericService<Employee>, IEmployeeService {
        public EmployeeService (ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
            : base (applicationDbContext, httpContextAccessor) {
        }
        public async Task<Employee> SaveEmployee(EmployeeModel employeeModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(employeeModel.Photo, 100, 100, "employee");
            Employee employee = ViewModelToEntity(employeeModel, ph);
            return (Employee) await AddAsync(employee);
        }
        public async Task<Employee> UpdateEmployee(EmployeeModel employeeModel) {
            PictureService pictureService = new PictureService();
            string[] ph = pictureService.SaveAndResizePicture(employeeModel.Photo, 100, 100, "employee");
            Employee employee = await FindAsync(employeeModel.id);
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Address = employeeModel.Address;
            employee.Mobile = employeeModel.Mobile;
            employee.Email = employeeModel.Email;
            employee.DateOfBirth = employeeModel.DateOfBirth;
            if (ph != null) {
                employee.Photo = ph[0];
                employee.PhotoResized = ph[1];
            }
            return (Employee) await UpdateAsync(employee);
        }
        public async Task<DataTableResult<Employee>> GetEmployeePage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Employee> list = context.Employees.Where(e => (e.FirstName.Contains(searchKey) || e.LastName.Contains(searchKey) ||
                    e.Mobile.Contains(searchKey) || searchKey == null) && !e.IsDeleted).Skip(start).Take(length)
                    .OrderByDescending(e => e.CreatedDate).Select(
                    e => new Employee {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Address = e.Address,
                        Mobile = e.Mobile,
                        Email = e.Email,
                        DateOfBirth = e.DateOfBirth,
                        Photo = e.Photo,
                        PhotoResized = e.PhotoResized,
                        Id = e.Id,
                    }).ToList();
            return new DataTableResult<Employee> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }
        private Employee ViewModelToEntity(EmployeeModel employeeModel, string[] ph) => new Employee {
            FirstName = employeeModel.FirstName,
            LastName = employeeModel.LastName,
            Address = employeeModel.Address,
            Mobile = employeeModel.Mobile,
            Email = employeeModel.Email,
            DateOfBirth = employeeModel.DateOfBirth,
            Photo = ph != null ? ph[0] : null,
            PhotoResized = ph != null ? ph[1] : null
        };

        public async Task<SelectResult> GetList(string key) {
            List<object> list = new List<object>();
            (await FindAllAsync(e => (e.FirstName.Contains(key) || e.LastName.Contains(key) || key == null) && !e.IsDeleted, 0, 50))
                .ToList().ForEach(e => list.Add(new { id = e.Id, text = $"{e.FirstName} {e.LastName}"}));
            return new SelectResult {
                results = list
            };
        }
    }
}
