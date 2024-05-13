using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Backend.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API for Categories: 
            //modelBuilder.Entity<Categories>().HasKey(category => category.categoryId);
            //modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
            //modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
            //Fluent API:
            ////////////////////////*User*////////////////////////
            modelBuilder.Entity<User>().HasKey(u => u.UserId);//PK
            modelBuilder.Entity<User>().Property(u => u.UserId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.IsAdmin).HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<User>().Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ////////////////////////*Category*////////////////////////
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);//PK
            modelBuilder.Entity<Category>().Property(c => c.CategoryId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Category>().Property(c => c.Description).HasMaxLength(0);
            ////////////////////////*Product*////////////////////////
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);//PK
            modelBuilder.Entity<Product>().Property(p => p.ProductId).IsRequired().ValueGeneratedOnAdd();//Generate id
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Color).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Size).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Brand).IsRequired();
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
            modelBuilder.Entity<Address>().Property(a => a.AddressLine).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.City).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.State).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.ZipCode).IsRequired();
            // ////////////////////////*Cart*////////////////////////
            // modelBuilder.Entity<Cart>().HasKey(c => c.CartId);
            // modelBuilder.Entity<Cart>().Property(c => c.CartId).IsRequired().ValueGeneratedOnAdd();
            // modelBuilder.Entity<Cart>().Property(c => c.UserId).IsRequired();
            // modelBuilder.Entity<Cart>().Property(c => c.Quantity).IsRequired();


            modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .IsRequired();

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
           .HasForeignKey(o => o.AddressId);
            //Category And Product:
            modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoriesId);
            //Order And Product:
            modelBuilder.Entity<Order>()
           .HasMany(o => o.Products)
           .WithMany(p => p.Orders)
           .UsingEntity(j => j.ToTable("OrderDetails"));
            //1 to 1:
            // {
            //     base.OnModelCreating(modelBuilder);

            //     modelBuilder.Entity<Categories>().HasKey(category => category.categoryId);

            //     modelBuilder.Entity<Categories>().Property(category => category.category_name).IsRequired();
            //     modelBuilder.Entity<Categories>().HasIndex(category => category.category_name).IsUnique();
            // }




        }
    }
}