
using Domain.Common;

namespace Domain.Entities {
    public class Sell : AuditableEntity {
        public Guid MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string? Remarks { get; set; }
        public DateTime SellDate { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public string? SellBill { get; set; }
    }
}
