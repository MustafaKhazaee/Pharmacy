
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities {
    public class Medicine : AuditableEntity {
        public Medicine() {
            this.Sells = new List<Sell>();
            this.Buys = new List<Buy>();
        }
        public string Name { get; set; }
        public string? ManufacturingCompany { get; set; }
        public string? Description { get; set; }
        public MedicineType Type { get; set; }
        public decimal Count { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int RemainingUsableDays { 
            get {
                TimeSpan timeSpan = ExpirationDate - DateTime.Now;
                return (int) timeSpan.TotalDays;
            } 
        }
        public DateTime ManufacturingDate { get; set; }
        public Category Category { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellProfitPercent { get; set; }
        public string? Photo { get; set; }
        public string? PhotoResized { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
