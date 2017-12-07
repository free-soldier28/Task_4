using System.Collections.Generic;

namespace WindowsService.DAL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string SecondName { get; set; }

        public virtual ICollection<Sales> Saleses { get; set; }
    }
}
