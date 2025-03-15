
using Infrastructure.Models.Bills;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Models.User;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<DanhMucLoaiHang> DanhMucLoaiHangs { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        //public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}

