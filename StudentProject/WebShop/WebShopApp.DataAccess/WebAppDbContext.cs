using Microsoft.EntityFrameworkCore;
using WebShopApp.Domain.Enums.UserEnum;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Userr> Users { get; set; }
        public DbSet<Productt> Products { get; set; }
        public DbSet<Orderr> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productt>(entity =>
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
               
       
            });


            modelBuilder.Entity<Userr>(entity =>
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

            


            modelBuilder.Entity<Orderr>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.Property(o => o.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);
            });


            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(oi => new { oi.OrderId, oi.ProductId })
                   .IsUnique();

            });

        }

    }
}
