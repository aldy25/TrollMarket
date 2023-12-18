using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrollMarket.DataAcces.Models;

public partial class TrollMarketContext : DbContext
{
    public TrollMarketContext()
    {
    }

    public TrollMarketContext(DbContextOptions<TrollMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<HistoryToUp> HistoryToUps { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NL8OBVKB;Initial Catalog=TrollMarket;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC079EDF7959");

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.Username, e.Role }, "UQ__Account__BECDD1F77615D950").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.BuyerNumber).HasName("PK__Buyer__2BE609BA1E1371D1");

            entity.ToTable("Buyer");

            entity.HasIndex(e => e.AccountId, "UQ__Buyer__349DA5A738300FD7").IsUnique();

            entity.Property(e => e.BuyerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Buyer)
                .HasForeignKey<Buyer>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Buyer__AccountId__2F10007B");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.BuyerNumber, e.ShipperNumber }).HasName("PK__CART__B95FA9DAE168C254");

            entity.ToTable("Cart");

            entity.Property(e => e.BuyerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ShipperNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.BuyerNumberNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.BuyerNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CART__BuyerNumbe__403A8C7D");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CART__ProductId__3F466844");

            entity.HasOne(d => d.ShipperNumberNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ShipperNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CART__ShipperNum__412EB0B6");
        });

        modelBuilder.Entity<HistoryToUp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HistoryT__3214EC077C0E1D96");

            entity.ToTable("HistoryToUp");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BuyerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TopUpDate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");

            entity.HasOne(d => d.BuyerNumberNavigation).WithMany(p => p.HistoryToUps)
                .HasForeignKey(d => d.BuyerNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HistoryTo__Buyer__33D4B598");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC076152BA18");

            entity.ToTable("Order");

            entity.Property(e => e.BuyerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
            entity.Property(e => e.ShipperNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.BuyerNumberNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__BuyerNumb__48CFD27E");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__ProductId__4AB81AF0");

            entity.HasOne(d => d.ShipperNumberNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__ShipperNu__49C3F6B7");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC076459AB7B");

            entity.ToTable("Product");

            entity.Property(e => e.Category)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Discontinue).HasDefaultValueSql("((0))");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.SellerNumberNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__SellerN__38996AB5");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerNumber).HasName("PK__Seller__2352305EFC0738E9");

            entity.ToTable("Seller");

            entity.HasIndex(e => e.AccountId, "UQ__Seller__349DA5A7F010C6F2").IsUnique();

            entity.Property(e => e.SellerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seller__AccountI__2A4B4B5E");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperNumber).HasName("PK__Shipper__ED0F8CBE9A43EC9D");

            entity.ToTable("Shipper");

            entity.Property(e => e.ShipperNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Service);
            entity.Property(e => e.ShipperName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
