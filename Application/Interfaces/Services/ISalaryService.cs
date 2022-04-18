
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface ISalaryService : IGenericService<Salary> {
        public Task<DataTableResult<object>> GetSalaryPage(DataTableParams param);
        public Task<Salary> SaveSalary (SalaryModel salaryModel);
    }
}
