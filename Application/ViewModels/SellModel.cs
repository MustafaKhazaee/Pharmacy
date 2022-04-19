
namespace Application.ViewModels {
    public class SellModel {
        public Guid Id { get; set; }
        public Guid MedicineId { get; set; }
        public Guid CustomerId { get; set; }
        public string? Remarks { get; set; }
        public DateTime SellDate { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
