using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Db
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Salary> Salary { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<People>()
                    .HasOne<Department>()
                    .WithMany()
                    .HasForeignKey(p => p.DepartmentId);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=capitalDB;Username=postgres;Password=postgres");
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual Department Department { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Salary
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public double Total { get; set; }
        public int Month { get; set; }
    }
}
