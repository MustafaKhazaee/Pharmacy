
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities {
    public class Salary : AuditableEntity {
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public Month Month { get; set; }
        public string Year { get; set; }
        public DateTime DatePaid { get; set; }
        public string Remarks { get; set; }

    }
}
