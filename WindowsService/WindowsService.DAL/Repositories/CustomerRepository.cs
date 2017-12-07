using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WindowsService.DAL.Entities;
using WindowsService.DAL.EntityFramework;
using WindowsService.DAL.Interfaces;

namespace WindowsService.DAL.Repositories
{
    public class CustomerRepository: IRepository<Customer>
    {
        private SalesContext db;

        public CustomerRepository(SalesContext context)
        {
            db = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers;
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> Find(Func<Customer,Boolean> predicate)
        {
            return db.Customers.Where(predicate).ToList();
        }

        public void Create(Customer item)
        {
            db.Customers.Add(item);
        }

        public void Update(Customer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Customer item = db.Customers.Find(id);
            db.Customers.Remove(item);
        }

    }
}
