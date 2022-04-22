
namespace Application.ViewModels {
    public class SellReportModel {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public Guid? customerId { get; set; }
        public Guid? medicineId { get; set; }
    }
}
