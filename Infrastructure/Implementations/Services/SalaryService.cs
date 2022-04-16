using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;
namespace Infrastructure.Implementations.Services {
    public class SalaryService : GenericService<Salary>, ISalaryService {
        public SalaryService (ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base (applicationDbContext, a) {}
        public async Task<DataTableResult<Salary>> GetSalaryPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<Salary> list = await FindAllAsync(e => (e.Employee.FirstName.Contains(searchKey) ||
                e.Employee.LastName.Contains(searchKey) || searchKey == null) && !e.IsDeleted, start, length);
            return new DataTableResult<Salary> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }

        public async Task<Salary> SaveSalary(SalaryModel salaryModel) {
            Salary salary = new Salary {
                EmployeeId = salaryModel.EmployeeId,
                Amount = salaryModel.Amount,
                Month = salaryModel.Month,
                Year = salaryModel.Year,
                Remarks = salaryModel.Remarks,
                DatePaid = salaryModel.DatePaid
            };
            return (Salary) await AddAsync(salary);
        }
    }
}
//tableTest = $('#example').DataTable({
//ajax:
//    {
//    url: "/datatables/getSomething",
//  type: "POST",
//  data: function(d) {
//            d.myCustomParams = myCustomParamsValues;
//        },
//},
//serverSide: true,
//processing: true,
//columns:
//    [
//  { data: 'a'},
//  { data: 'b'},
//  { data: 'c'},
//  { data: 'd'}
//],
//responsive: true
//});