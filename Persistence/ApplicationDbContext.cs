
using Application.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Buy> Buys { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            string salt = "".GetSalt();
            List<User> defaultUser = new List<User> {
                new User {
                    Id = Guid.NewGuid(), UserName = "mustafa", Password = $"Root_Mustafa@123{salt}".GetHash(),
                    Salt = salt, CreatedBy = "A", Email = "mustafa.khazaee1@gmail.com", CreatedDate = DateTime.Now,
                    firstName = "Mustafa", lastName = "Khazaee", Role = Role.root
                },
            };
            modelBuilder.Entity<User>().HasData(defaultUser);
        }
    }
}
