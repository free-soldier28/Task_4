using System;

namespace WindowsService.BLL.DTO
{
    public class SalesDTO
    {
        public int Id { get; set; }
        public int IdManager { get; set; }
        public int IdCustomer { get; set; }
        public int IdProduct { get; set; }
        public DateTime Date { get; set; }
    }
}
