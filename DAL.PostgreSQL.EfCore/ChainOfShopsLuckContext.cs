using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.PostgreSQL.EfCore;

public partial class ChainOfShopsLuckContext : DbContext
{
    public ChainOfShopsLuckContext()
    {
    }

    public ChainOfShopsLuckContext(DbContextOptions<ChainOfShopsLuckContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CashReceipt> CashReceipts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<ReceiptProduct> ReceiptProducts { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cash_receipts_pkey");

            entity.ToTable("cash_receipts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.CashReceipts)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("cash_receipts_shop_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("1")
                .HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("products_category_id_fkey");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_stock_pkey");

            entity.ToTable("product_stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_stock_product_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("product_stock_shop_id_fkey");
        });

        modelBuilder.Entity<ReceiptProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("receipt_products_pkey");

            entity.ToTable("receipt_products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountProducts).HasColumnName("count_products");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("receipt_products_product_id_fkey");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptProducts)
                .HasForeignKey(d => d.ReceiptId)
                .HasConstraintName("receipt_products_receipt_id_fkey");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shops_pkey");

            entity.ToTable("shops");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
