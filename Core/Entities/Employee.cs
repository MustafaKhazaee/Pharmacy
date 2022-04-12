
using Domain.Common;

namespace Domain.Entities {
    public class Employee : AuditableEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? PhotoResized { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
