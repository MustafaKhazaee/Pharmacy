
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface IEmployeeService : IGenericService<Employee> {
        public Task<Employee> SaveEmployee(EmployeeModel employeeModel);
        public Task<IEnumerable<Employee>> GetEmployeePage(string firstName, string lastName, string email, string mobile, int pageIndex, int pageSize);
    }
}
