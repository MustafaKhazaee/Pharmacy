
using Domain.Common;

namespace Domain.Entities {
    public class Buy : AuditableEntity {
        public Guid MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string? Remarks { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }  //  باقی داری
        public string? BuyBill { get; set; }
    }
}
