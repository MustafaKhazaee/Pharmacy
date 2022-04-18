using Application.Common;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Persistence;
using System.Linq.Expressions;

namespace Infrastructure.Common {
    public class GenericService<T> : IGenericService<T> where T : AuditableEntity {
        protected readonly ApplicationDbContext context;
        protected readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        public GenericService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor) {
            this.context = applicationDbContext;
            this.unitOfWork = new UnitOfWork(this.context);
            this.httpContextAccessor = httpContextAccessor;
        }
        #region Common CRUD Operation :
        public async Task<object> AddAsync(T entity) {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities) {
            foreach (var entity in entities) {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            }
            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
        public async Task<int> CountAsync() => await Task.FromResult(context.Set<T>().Count(t => !t.IsDeleted));
        public async Task<T> FindAsync(Guid Id) => 
            await context.Set<T>().FindAsync(Id);
        public async Task<IEnumerable<T>> GetAllAsync() => await Task.FromResult(context.Set<T>().ToList());
        public async Task RemoveAsync(Guid Id) {
            await Task.FromResult(context.Set<T>().Remove(await FindAsync(Id)));
            await context.SaveChangesAsync();
        }
        public async Task RemoveRangeAsync(IEnumerable<T> entities) {
            context.Set<T>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }
        public async Task<object> UpdateAsync(T entity) {
            entity.LastModifiedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.LastModifiedDate = DateTime.UtcNow;
            await Task.FromResult(context.Update(entity));
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int start, int length) => 
            await Task.FromResult(context.Set<T>().Where(predicate).Skip(start).Take(length).OrderByDescending(t => t.CreatedDate).ToList());
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) => 
            await Task.FromResult(context.Set<T>().Where(predicate).FirstOrDefault());
        #endregion

        #region AuditableEntity Operations :
        public async Task<T> SoftDeleteAsync(Guid Id) {
            T auditableEntiy = await context.Set<T>().FindAsync(Id);
            auditableEntiy.IsDeleted = true;
            auditableEntiy.DeletedDate = DateTime.UtcNow;
            auditableEntiy.DeletedBy = this.httpContextAccessor.HttpContext.User.Identity.Name;
            await context.SaveChangesAsync();
            return auditableEntiy;
        }
        //public async Task<T> ModifyAsync(Guid Id) {
        //    T auditableEntiy = await context.Set<T>().FindAsync(Id);
        //    auditableEntiy.LastModifiedDate = DateTime.UtcNow;
        //    auditableEntiy.LastModifiedBy = httpContextAccessor.HttpContext.User.Identity.Name;
        //    await context.SaveChangesAsync();
        //    return auditableEntiy;
        //}
        #endregion
    }
}
