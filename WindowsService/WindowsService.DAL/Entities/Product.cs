using System.Collections.Generic;

namespace WindowsService.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Sales> Saleses { get; set; }
    }
}
