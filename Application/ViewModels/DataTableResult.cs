
using Domain.Common;

namespace Application.ViewModels {
    public class DataTableResult<T> where T : AuditableEntity {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string Error { get; set; }
        public string PartialView { get; set; }
    }
}
