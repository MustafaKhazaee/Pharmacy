
using Domain.Common;

namespace Application.ViewModels {
    public class DataTableResult<T> where T : AuditableEntity {
        public DataTableResult (IEnumerable<T> _list) {
            list = _list;
            rowSize = list.Count();
        }
        public IEnumerable<T> list;
        public int rowSize;
    }
}
