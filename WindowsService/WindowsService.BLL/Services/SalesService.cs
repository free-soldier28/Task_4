using System;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using AutoMapper;
using Entities;

namespace WindowsService.BLL
{
    public class SalesService: ISalesService
    {
        IUnitOfWork Database { get; set; }

        public SalesService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddSales(string managerName, DateTime date, string[] substrings)
        {
            int idManagerDTO = Database.Managers.Find(x => x.SecondName == managerName).Select(z => z.Id).FirstOrDefault();
            int idCustomerDTO = Database.Customers.Find(x => x.FullName == substrings[1]).Select(z => z.Id).FirstOrDefault();
            int idProductDTO = Database.Products.Find(x => x.Name == substrings[2]).Select(z => z.Id).FirstOrDefault();

            if (idManagerDTO == 0)
            {
                ManagerDTO managerDto = new ManagerDTO()
                {
                    Id = 0,
                    SecondName = managerName
                };

                Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, Manager>());
                var manager = Mapper.Map<ManagerDTO, Manager>(managerDto);
                Database.Managers.Create(manager);
                Database.Save();

                idManagerDTO = Database.Managers.Find(x => x.SecondName == managerName).Select(z => z.Id).FirstOrDefault();
            }

            if (idCustomerDTO == 0)
            {
                CustomerDTO customerDto = new CustomerDTO()
                {
                    Id = 0,
                    FullName = substrings[1]
                };

                Mapper.Initialize(cfg => cfg.CreateMap<CustomerDTO, Customer>());
                var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
                Database.Customers.Create(customer);
                Database.Save();

                idCustomerDTO = Database.Customers.Find(x => x.FullName == substrings[1]).Select(z => z.Id).FirstOrDefault();
            }

            if (idProductDTO == 0)
            {
                ProductDTO productDto = new ProductDTO()
                {
                    Id = 0,
                    Name = substrings[2]
                };

                Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
                var product = Mapper.Map<ProductDTO, Product>(productDto);
                Database.Products.Create(product);
                Database.Save();

                idProductDTO = Database.Products.Find(x => x.Name == substrings[2]).Select(z => z.Id).FirstOrDefault();
            }


            SalesDTO salesDto = new SalesDTO
            {
                Id = 0,
                IdManager = idManagerDTO,
                IdCustomer = idCustomerDTO,
                IdProduct = idProductDTO,
                Date = Convert.ToDateTime(substrings[0]),
                Amount = Convert.ToDouble(substrings[3])
            };

            Mapper.Initialize(cfg => cfg.CreateMap<SalesDTO, Sales>());
            var sales = Mapper.Map<SalesDTO,Sales>(salesDto);

            Database.Saleses.Create(sales);
            Database.Save();
        }
    }
}
