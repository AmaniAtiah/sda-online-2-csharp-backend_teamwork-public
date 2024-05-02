using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<Categories> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Order_Item> Order_Items { get; set; }
        //public DbSet<Wishlist> Wishlists { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.category_id); 
        }

    }
}
