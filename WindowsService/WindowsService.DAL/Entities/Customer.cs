using System.Collections.Generic;

namespace WindowsService.DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<Sales> Saleses { get; set; }
    }
}
