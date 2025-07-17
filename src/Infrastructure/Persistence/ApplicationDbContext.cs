using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    // DbContextOptions<TContext>: object chứa các cấu hình để thiết lập DbContext
    // base() được dùng để gọi constructor của lớp cha (DbContext)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }    // Đại diện cho bảng `Products` trong cơ sở dữ liệu

    protected override void OnModelCreating(ModelBuilder modelBuilder)  // cấu hình schema
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);   //bắt buộc, tối đa 200 ký tự
            entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        base.OnModelCreating(modelBuilder);
    }
}