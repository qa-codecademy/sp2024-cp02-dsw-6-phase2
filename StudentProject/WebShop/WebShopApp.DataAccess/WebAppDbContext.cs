using Microsoft.EntityFrameworkCore;
using WebShopApp.Domain.Enums.UserEnum;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                // Configure primary key
                entity.HasKey(p => p.Id);

                // Configure properties
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(p => p.ProductDescription)
                    .HasMaxLength(1000);

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Category)
                    .IsRequired();

                entity.Property(p => p.Brand)
                    .HasMaxLength(100);

                entity.Property(p => p.QuantityAvailable)
                    .IsRequired();

                entity.Property(p => p.ShippingCost)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.ShippingTime)
                    .IsRequired();

                entity.Property(p => p.OriginalImagePath)
                    .HasMaxLength(500);

                entity.Property(p => p.SavedImagePath)
                    .HasMaxLength(500);

                // Configure optional properties
                entity.Property(p => p.Discount)
                    .IsRequired(false);

                // Configure relationships
                // Assuming one-to-many relationships with Users and Orders
                entity.HasMany(p => p.Users)
                    .WithMany(u => u.Products)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductUser",
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                        j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"));


                entity.HasMany(p => p.Orders)
                    .WithMany(o => o.Products)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderProduct",
                        j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                        j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"));




            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                .IsRequired().
                HasMaxLength(50);

                entity.Property(u => u.LastName)
               .IsRequired().
               HasMaxLength(70);

                entity.Property(u => u.Address)
               .IsRequired().
               HasMaxLength(150);


                entity.Property(u => u.UserName)
               .IsRequired().
               HasMaxLength(50);


                entity.Property(u => u.Email).
                IsRequired().
                HasMaxLength(200);

                entity.HasIndex(u => u.Email)
                .IsUnique();

                entity.Property(u => u.Role).
                HasDefaultValue(RoleEnum.User);

                entity.Property(u => u.Phone).
                IsRequired().
                HasMaxLength(20);

                entity.Property(u => u.Password)
                .IsRequired();

                // One-to-one relationship between User and Cart
                entity.HasOne(u => u.Cart)
                    .WithOne(c => c.User)
                    .HasForeignKey<Cart>(c => c.UserId) // Foreign key in Cart entity
                    .OnDelete(DeleteBehavior.Cascade);  // Deleting a User deletes their Cart




            });



            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.Id);

                // One-to-one relationship with User
                entity.HasOne(c => c.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Cart>(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.CartItems)
                    .WithOne(ci => ci.Cart)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(c => c.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0.00m);
            });

            // Configure CartItem and Product relationship
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(ci => ci.Product)
                    .WithMany()  // A Product can be in many CartItems
                    .HasForeignKey(ci => ci.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Ensure each Product appears only once in a Cart
                entity.HasIndex(ci => new { ci.CartId, ci.ProductId })
                    .IsUnique();
            });



            //// Configure Cart and CartItem relationship
            //modelBuilder.Entity<Cart>()
            //    .HasMany(c => c.CartItems)           // A Cart has many CartItems
            //    .WithOne(ci => ci.Cart)              // Each CartItem belongs to one Cart
            //    .HasForeignKey(ci => ci.CartId)      // Foreign key in CartItem is CartId
            //    .OnDelete(DeleteBehavior.Cascade);   // Deleting a Cart deletes related CartItems

            //modelBuilder.Entity<Cart>()
            //         .Property(c => c.TotalAmount)
            //        .HasColumnType("decimal(18,2)");

            //modelBuilder.Entity<Cart>()
            //         .Property(c => c.TotalAmount)
            //         .HasDefaultValue(0.00m);

            //// Configure CartItem and Product relationship
            //modelBuilder.Entity<CartItem>()
            //    .HasOne(ci => ci.Product)            // Each CartItem is related to one Product
            //    .WithMany()                          // A Product can be in many CartItems
            //    .HasForeignKey(ci => ci.ProductId)   // Foreign key in CartItem is ProductId
            //    .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting Product if referenced by CartItem

            //// Optional: Index to ensure each Product appears once in a Cart
            //modelBuilder.Entity<CartItem>()
            //    .HasIndex(ci => new { ci.CartId, ci.ProductId })
            //    .IsUnique(); // Ensures that a CartItem with the same CartId and ProductId is unique


        }

    }
}
