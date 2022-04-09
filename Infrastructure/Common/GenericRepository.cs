
using Application.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Persistence;
namespace Infrastructure.Common {
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntity {
        protected readonly ApplicationDbContext context;
        public GenericRepository(ApplicationDbContext _context) => context = _context;

        #region Common CRUD Operation :
        public async Task<object> AddAsync(T entity) {
            return await context.Set<T>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities) => await context.Set<T>().AddRangeAsync(entities);
        public async Task<int> CountAsync() => await Task.FromResult(context.Set<T>().Count());
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate) => await Task.FromResult(context.Set<T>().Where(predicate));
        public async Task<T> FindAsync(Guid Id) => await context.Set<T>().FindAsync(Id);
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) => await context.Set<T>().FindAsync(predicate);
        public async Task<IEnumerable<T>> GetAllAsync() => await Task.FromResult(context.Set<T>().ToList());
        public async Task RemoveAsync(Guid Id) => await Task.FromResult(context.Set<T>().Remove(await FindAsync(Id)));
        public void RemoveRange(IEnumerable<T> entities) => context.Set<T>().RemoveRange(entities);
        #endregion

        #region AuditableEntity Operations :
        public async Task<T> SoftDeleteAsync(Guid Id, string DeleteBy) {
            T auditableEntiy = await context.Set<T>().FindAsync(Id);
            auditableEntiy.IsDeleted = true;
            auditableEntiy.DeletedDate = DateTime.UtcNow;
            auditableEntiy.DeletedBy = DeleteBy;
            return auditableEntiy;
        }
        public async Task<T> ModifyAsync(Guid Id, string ModifiedBy) {
            T auditableEntiy = await context.Set<T>().FindAsync(Id);
            auditableEntiy.LastModifiedDate = DateTime.UtcNow;
            auditableEntiy.LastModifiedBy = ModifiedBy;
            return auditableEntiy;
        }
        #endregion

        #region State Change :
        public void MarkAsAdded(T entity) => context.Entry<T>(entity).State = EntityState.Added;
        public void MarkAsDeleted(T entity) => context.Entry<T>(entity).State = EntityState.Deleted;
        public void MarkAsDetached(T entity) => context.Entry<T>(entity).State = EntityState.Detached;
        public void MarkAsModified(T entity) => context.Entry<T>(entity).State = EntityState.Modified;
        public void MarkAsUnchanged(T entity) => context.Entry<T>(entity).State = EntityState.Unchanged;
        #endregion
    }
}
