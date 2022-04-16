
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities {
    public class Transaction : AuditableEntity {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
