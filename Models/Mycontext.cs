using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models {
    public class MyContext : DbContext {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext (DbContextOptions options) : base (options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Orders> Orders { get; set; }

    }
}