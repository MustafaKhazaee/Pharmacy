
using Domain.Enums;

namespace Application.ViewModels {
    public class SalaryReportModel {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
    }
}
