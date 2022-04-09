
using Domain.Common;

namespace Domain.Entities {
    public class Customer : AuditableEntity {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        
    }
}
