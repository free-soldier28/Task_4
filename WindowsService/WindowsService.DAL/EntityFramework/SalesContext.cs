using System.Data.Entity;
using WindowsService.DAL.Entities;

namespace WindowsService.DAL.EntityFramework
{
    public class SalesContext: DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sales> Saleses { get; set; }

        public SalesContext(string connectionsString) : base(connectionsString)
        {
            
        }
    }
}
