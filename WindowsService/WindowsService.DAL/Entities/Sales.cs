using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.DAL.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int IdManager { get; set; }
        public int IdCustomer { get; set; }
        public int IdProduct { get; set; }
        public DateTime Date { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
