
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities {
    public class User : AuditableEntity {
        public string UserName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string? Email { get; set; }
        public bool IsLocked { get; set; } = false;
        public Role Role { get; set; }
    }
}
