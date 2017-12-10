
using System;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.DAL.Interfaces;
using WindowsService.DAL.Repositories;

namespace WindowsService.BLL
{
    public class SalesService
    {
        IUnitOfWork Database { get; set; }

        public SalesService()
        {
            //Database = uow;
        }

        public void AddSales(string managerName, DateTime date, string[] substrings)
        {
            ManagerRepository managerRepository = new ManagerRepository();
            int idManager = managerRepository.Find(x => x.SecondName == managerName).Select(z => z.Id).FirstOrDefault();

            CustomerRepository customerRepository = new CustomerRepository();
            int idCustomer = customerRepository.Find(x => x.FullName == substrings[1]).Select(z => z.Id).FirstOrDefault();

            ProductRepository productRepository = new ProductRepository();
            int idProduct = productRepository.Find(x => x.Name = substrings[2]).Select(z => z.Id).FirstOrDefault();


            SalesRepository salesRepository = new SalesRepository();
            SalesDTO salesDto = new SalesDTO
            {
                Id = 0,
                IdManager = idManager,
                IdCustomer = idCustomer,
                IdProduct = idProduct,
                Date = Convert.ToDateTime(substrings[0]),
                Amount = Convert.ToDouble(substrings[3])
            };
            salesRepository.Create(salesDto);
        }
    }
}
