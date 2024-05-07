// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;

// namespace api.Data
// {
//     public class AppDbContext1 : DbContext
//     {
//         public AppDbContext1(DbContextOptions<AppDbContext1> options) : base(options) { }

//         public DbSet<User> Users { get; set; }
//         //public DbSet<Address> Addresses { get; set; }
//         public DbSet<Categories> Categories { get; set; }
//         public DbSet<Product> Products { get; set; }
//         //public DbSet<Order> Orders { get; set; }
//         //public DbSet<Order_Item> Order_Items { get; set; }
//         //public DbSet<Wishlist> Wishlists { get; set; }
//         //public DbSet<Cart> Carts { get; set; }
//         public DbSet<Payment> Payments { get; set; }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             //Fluent API for Categories: 
//             modelBuilder.Entity<Categories>()

//                 .HasKey(c => c.category_id);
//             //Fluent API for Product:    
//             modelBuilder.Entity<Product>().HasKey(productId => productId.ProductId);//PK
//             modelBuilder.Entity<Product>().Property(product => product.ProductId).IsRequired().ValueGeneratedOnAdd();//Generate id
//             modelBuilder.Entity<Product>().Property(productName => productName.Name).IsRequired().HasMaxLength(100);
//             modelBuilder.Entity<Product>().Property(productDescription => productDescription.Description).IsRequired();
//             modelBuilder.Entity<Product>().Property(productPrice => productPrice.Price).IsRequired();
//             modelBuilder.Entity<Product>().Property(productQuanttity => productQuanttity.Quantity).IsRequired();

//             //Relations:
//             //1 to 1:
//             //modelBuilder.Entity<Product>().HasOne(product => product.Categories).WithOne(categories => categories.product).HasForeingKey(categories => categories.productId);


//             /*  modelBuilder.Entity<User>(entity =>
//              {
//                  entity.HasKey(e => e.UserId);

//                  entity.Property(e => e.UserName)
//                  .IsRequired()
//                  .HasMaxLength(32);

//                  entity.Property(e => e.FirstName)
//                  .IsRequired()
//                  .HasMaxLength(32);

//                  entity.Property(e => e.LastName)
//                  .IsRequired()
//                  .HasMaxLength(32);

//                  entity.Property(user => user.CreatedAt)
//                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

//                  entity.Property(user => user.UpdatedAt)
//                  .HasDefaultValueSql("CURRENT_TIMESTAMP");



//                  entity.Property(e => e.Email)
//                  .IsRequired()
//                  .HasMaxLength(255);

//                  entity.HasIndex(e => e.Email).IsUnique();

//                  entity.HasIndex(e => e.UserName).IsUnique();
//                  entity.HasIndex(e => e.PhoneNumber).IsUnique();



//                  entity.Property(e => e.Password)
//                      .IsRequired()
//                      .HasMaxLength(255);

//                  entity.Property(e => e.PhoneNumber).HasMaxLength(20);

//                  entity.Property(e => e.IsAdmin)
//                  .HasDefaultValue(false);


//              }); */

//         }

//     }
// }
