using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
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

            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(32);

                entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(32);

                entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(32);

                entity.Property(user => user.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(user => user.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");



                entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

                 entity.HasIndex(e => e.Email).IsUnique();

                 entity.HasIndex(e => e.UserName).IsUnique();
                 entity.HasIndex(e => e.PhoneNumber).IsUnique();



                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.IsAdmin)
                .HasDefaultValue(false);


            });
        }

    }
}
