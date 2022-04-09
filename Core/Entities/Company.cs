
using Domain.Common;

namespace Domain.Entities {
    public class Company : AuditableEntity {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
