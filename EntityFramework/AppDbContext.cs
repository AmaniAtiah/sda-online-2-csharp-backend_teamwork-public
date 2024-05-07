
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        //public DbSet<CategoryTable> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Order_Item> Order_Items { get; set; }
        //public DbSet<Wishlist> Wishlists { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API for Categories: 
            //modelBuilder.Entity<Categories>().HasKey(category => category.category_id);
            //modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
            //modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
            //Fluent API for Product:    
            modelBuilder.Entity<Product>().HasKey(productId => productId.ProductId);//PK
            modelBuilder.Entity<Product>().Property(product => product.ProductId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<Product>().Property(productName => productName.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(productDescription => productDescription.Description).IsRequired();
            modelBuilder.Entity<Product>().Property(productPrice => productPrice.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(productQuanttity => productQuanttity.Quantity).IsRequired();

            //Relations:
            //1 to 1:
            //modelBuilder.Entity<Product>().HasOne(product => product.Categories).WithOne(categories => categories.product).HasForeingKey(categories => categories.productId);

            // {
            //     base.OnModelCreating(modelBuilder);

            //     modelBuilder.Entity<Categories>().HasKey(category => category.category_id);

            //     modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
            //     modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
            // }



        }
    }
}
