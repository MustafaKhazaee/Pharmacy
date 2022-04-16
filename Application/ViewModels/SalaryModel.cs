
using Domain.Enums;

namespace Application.ViewModels {
    public class SalaryModel {
        public Guid EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public DateTime DatePaid { get; set; }
        public string? Remarks { get; set; }
    }
}
