namespace Entities
{
    using System.Data.Entity;

    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=SalesContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sales> Saleses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.FullName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Saleses)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.IdCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.SecondName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .HasMany(e => e.Saleses)
                .WithRequired(e => e.Manager)
                .HasForeignKey(e => e.IdManager)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Saleses)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);
        }
    }
}
