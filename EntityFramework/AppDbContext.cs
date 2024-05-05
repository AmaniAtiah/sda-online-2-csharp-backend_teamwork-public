using Microsoft.EntityFrameworkCore;

namespace Backend.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<CategoryTable> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Order_Item> Order_Items { get; set; }
        //public DbSet<Wishlist> Wishlists { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categories>().HasKey(category => category.category_id);

        modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
        modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
        }

    }
}
