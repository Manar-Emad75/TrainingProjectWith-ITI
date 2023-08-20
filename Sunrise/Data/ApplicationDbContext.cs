using Microsoft.EntityFrameworkCore;
using Sunrise.Models;
namespace Sunrise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
       
    }
}








//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//	base.OnConfiguring(optionsBuilder);
//	optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Sunrise;Trusted_Connection=True;TrustServerCertificate=True");
//}