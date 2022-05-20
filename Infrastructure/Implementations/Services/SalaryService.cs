using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Persistence;
namespace Infrastructure.Implementations.Services {
    public class SalaryService : GenericService<Salary>, ISalaryService {
        public SalaryService (ApplicationDbContext applicationDbContext, IHttpContextAccessor a) : base (applicationDbContext, a) {}
        public async Task<DataTableResult<object>> GetSalaryPage(DataTableParams param) {
            string searchKey = param.Search.Value;
            int start = param.Start, length = param.Length, all = await CountAsync();
            IEnumerable<object> list = await Task.FromResult(context.Salaries.Where(e => (e.Employee.FirstName.Contains(searchKey)
                || e.Employee.LastName.Contains(searchKey) || searchKey == null) && !e.IsDeleted).Select(s => new {
                    id = s.Id,
                    fullName = $"{s.Employee.FirstName} {s.Employee.LastName}",
                    amount = s.Amount,
                    paidDate = s.DatePaid,
                    month = s.Month,
                    year = s.Year,
                    remarks = s.Remarks
                }).Skip(start).Take(length).ToList());
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = all,
                RecordsTotal = all,
                Draw = param.Draw
            };
        }

        public async Task<DataTableResult<object>> GetSalaryReport(SalaryReportModel salaryReportModel) {
            DateTime? fromDate = salaryReportModel.fromDate, toDate = salaryReportModel.toDate;
            Guid? empId = salaryReportModel.EmployeeId;
            Month month = salaryReportModel.Month;
            int year = salaryReportModel.Year;
            int count = await CountAsync();
            List<object> list = context.Salaries.Where(b => (empId == null || empId == b.EmployeeId) &&
                            (fromDate == null || fromDate <= b.DatePaid) &&
                            (toDate == null || toDate >= b.DatePaid) &&
                            (year == -1 || year == b.Year) &&
                            (month == 0 || month == b.Month)
                 )
                .GroupBy(b => new {
                    eFirstName = b.Employee.FirstName,
                    eLastName = b.Employee.LastName,
                    Month = b.Month,
                    Year = b.Year,
                })
               .Select(b => new {
                   year = b.Key.Year,
                   month = b.Key.Month,
                   employee = $"{b.Key.eFirstName} {b.Key.eLastName}",
                   amount = b.Sum(b => b.Amount),
               }).ToList<object>();
            int totalPrice = 0;
            foreach (dynamic i in list) {
                totalPrice += i.amount;
            }
            list.Add(new {
                year = "",
                month = "",
                employee = "مجموعه",
                amount = totalPrice
            });
            return new DataTableResult<object> {
                Data = list,
                RecordsFiltered = list.Count(),
                RecordsTotal = list.Count(),
                Draw = 1
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