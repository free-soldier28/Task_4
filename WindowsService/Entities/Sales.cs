namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Saleses")]
    public partial class Sales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IdManager { get; set; }

        public int IdCustomer { get; set; }

        public int IdProduct { get; set; }
        public double Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Product Product { get; set; }
    }
}
