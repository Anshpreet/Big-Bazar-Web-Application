using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BigBazarEntities.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

#nullable disable

namespace BigBazarRepository 
{
    public partial class BigBazarContext : DbContext
    {
        public BigBazarContext()
        {
        }

        public BigBazarContext(DbContextOptions<BigBazarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Reciept> Reciepts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=LAPTOP-CG5B925T; database=BigBazar;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.HasOne(d => d.Category)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.CategoryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.Property(e => e.DateOfPurchase).HasColumnType("date");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.Purchases)
                //    .HasForeignKey(d => d.CustomerId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Purchase_Customer");

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.Purchases)
                //    .HasForeignKey(d => d.ProductId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Purchase_Product");
            });

            modelBuilder.Entity<Reciept>(entity =>
            {
                entity.ToTable("Reciept");

                entity.Property(e => e.Date).HasColumnType("date");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.Reciepts)
                //    .HasForeignKey(d => d.CustomerId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Reciept_Customer");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>().ToTable("Product");
           // modelBuilder..Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
