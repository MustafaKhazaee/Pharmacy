using Domain.Common;
using System.Linq.Expressions;
namespace Application.Common {
    public interface IGenericRepository<T> where T : AuditableEntity {
        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        public Task<T> FindAsync(Guid Id);
        public Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<object> AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public Task RemoveAsync(Guid Id);
        public Task<T> SoftDeleteAsync(Guid Id, string DeleteBy);
        public Task<T> ModifyAsync(Guid Id, string ModifiedBy);
        public void RemoveRange(IEnumerable<T> entities);
        public Task<int> CountAsync();
        public void MarkAsDetached(T entity);
        public void MarkAsUnchanged(T entity);
        public void MarkAsModified(T entity);
        public void MarkAsDeleted(T entity);
        public void MarkAsAdded(T entity);
    }
}

