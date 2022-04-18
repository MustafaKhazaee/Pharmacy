
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface IEmployeeService : IGenericService<Employee> {
        public Task<Employee> SaveEmployee(EmployeeModel employeeModel);
        public Task<Employee> UpdateEmployee(EmployeeModel employeeModel);
        public Task<DataTableResult<Employee>> GetEmployeePage(DataTableParams param);
        public Task<SelectResult> GetList (string key);
    }
}
