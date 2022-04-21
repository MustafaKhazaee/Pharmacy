
namespace Application.ViewModels {
    public class BuyReportModel {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public Guid? companyId { get; set; }
        public Guid? medicineId { get; set; }
    }
}
