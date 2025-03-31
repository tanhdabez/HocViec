using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly AppDbContext _context;
        public HoaDonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<HoaDon> CreateHoaDon(HoaDon hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();
            return hoaDon;
        }

        public async Task<HoaDon?> GetHoaDon(Guid hoaDonId)
        {
            return await _context.HoaDons.FirstOrDefaultAsync(x => x.Id == hoaDonId);
        }

        public async Task<HoaDon> UpdateHoaDon(HoaDon hoaDon)
        {
            _context.HoaDons.Update(hoaDon);
            await _context.SaveChangesAsync();
            return hoaDon;
        }

        public async Task<int> GetTotalCount()
        {
            return await _context.HoaDons.CountAsync();
        }

        public async Task<List<HoaDon>> GetAllHoaDons(DateTime? startDate, DateTime? endDate, int? trangThai)
        {
            var query = _context.HoaDons.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.CreatedDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.CreatedDate <= endDate.Value);

            if (trangThai.HasValue)
                query = query.Where(x => x.TrangThai == trangThai.Value);

            return await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
        public async Task<List<ChiTietHoaDon?>> GetChiTietHoaDon(Guid idHoaDon)
        {
            var chiTietHoaDons = await _context.ChiTietHoaDons
            .Where(x => x.HoaDonId == idHoaDon)
            .Include(x => x.SanPham) 
            .AsNoTracking() // Nếu chỉ đọc dữ liệu
            .ToListAsync();

            if (chiTietHoaDons == null || chiTietHoaDons.Count == 0)
            {
                return null; // Hoặc ném ngoại lệ, trả về thông báo lỗi
            }

            return chiTietHoaDons;
        }


        public async Task CreateChiTietHoaDon(List<ChiTietHoaDon> chiTietHoaDon)
        {
            _context.ChiTietHoaDons.AddRange(chiTietHoaDon);
            foreach (var item in chiTietHoaDon)
            {
                var sanPham = await _context.SanPhams.FindAsync(item.SanPhamId);
                sanPham.SoLuong -= item.SoLuong;
                if (sanPham.SoLuong == 0)
                {
                    sanPham.TrangThai = false;
                }
            }
            await _context.SaveChangesAsync();
        }

       
        public async Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonId(Guid hoaDonId)
        {
            return await _context.ChiTietHoaDons.Where(ct => ct.HoaDonId == hoaDonId).ToListAsync();
        }
        public async Task<bool> RemoveProductInCart(Guid? userId, Guid productId)
        {
            var cartUser = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartItemsUser = await _context.CartItems.Where(x => x.CartId == cartUser.Id && x.SanPhamId == productId).ToListAsync();
            if (cartItemsUser != null)
            {
                _context.CartItems.RemoveRange(cartItemsUser);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string GenerateInvoiceCode(string prefix)
        {
            string invoiceId = Guid.NewGuid().ToString("N");
            // Sử dụng format "N" để loại bỏ dấu gạch ngang, sau đó lấy 8 ký tự đầu
            return prefix + "-" + invoiceId.Substring(0, 8).ToUpper();
        }
    }
}
