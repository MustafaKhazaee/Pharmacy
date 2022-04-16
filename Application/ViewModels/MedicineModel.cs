
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels {
    public class MedicineModel {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string? ManufacturingCompany { get; set; }
        public string? Description { get; set; }
        public MedicineType Type { get; set; }
        public decimal Count { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public Category Category { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellProfitPercent { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
