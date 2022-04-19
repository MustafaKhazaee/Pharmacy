
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels {
    public class BuyModel {
        public Guid Id { get; set; }
        public Guid MedicineId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }  //  باقی داری
        public IFormFile? BuyBill { get; set; }
        public string? Remarks { get; set; }
    }
}
