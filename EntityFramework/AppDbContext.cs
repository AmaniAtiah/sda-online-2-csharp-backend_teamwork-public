
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
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Order_Item> Order_Items { get; set; }
        //public DbSet<Wishlist> Wishlists { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API for Categories: 
            modelBuilder.Entity<Categories>().HasKey(category => category.category_id);
            //modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
            //modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
            //Fluent API:
            ////////////////////////*User*////////////////////////
            modelBuilder.Entity<User>().HasKey(u => u.UserId);//PK
            modelBuilder.Entity<User>().Property(u => u.UserId).IsRequired().ValueGeneratedOnAdd();//Generate id
            ////////////////////////*Category*////////////////////////
            modelBuilder.Entity<Categories>().HasKey(c => c.category_id);//PK
            modelBuilder.Entity<Categories>().Property(c => c.category_id).IsRequired().ValueGeneratedOnAdd();//Generate id
            ////////////////////////*Product*////////////////////////
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);//PK
            modelBuilder.Entity<Product>().Property(p => p.ProductId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Quantity).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.CreateAt).HasDefaultValue(DateTime.UtcNow);//Generate date
            ////////////////////////*Order*////////////////////////
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);//PK
            modelBuilder.Entity<Order>().Property(o => o.OrderId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<Order>().Property(o => o.TotalPrice).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Status).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasDefaultValue(DateTime.UtcNow);//Generate date
            ////////////////////////*Address*////////////////////////
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);//PK
            modelBuilder.Entity<Address>().Property(a => a.AddressId).IsRequired().ValueGeneratedOnAdd();//Generate id
            //Relations:
            //1 to many
            //User And Addresses:
            modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
            //User And Order:
            modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
            //Addresses And Order:
            modelBuilder.Entity<Address>()
            .HasMany(a => a.Orders)
            .WithOne(o => o.Addresses)
            .HasForeignKey(o => o.AddresseId);
            //Category And Product:
            modelBuilder.Entity<Categories>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoriesId);
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
