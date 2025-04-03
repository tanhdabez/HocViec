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

        public async Task<bool> UpdateHoaDon(HoaDon hoaDon)
        {
            var dataHoaDon = await _context.HoaDons.FirstOrDefaultAsync(x => x.Id == hoaDon.Id);
            var dataCTHoaDon = await _context.ChiTietHoaDons.Where(x => x.HoaDonId == hoaDon.Id).ToListAsync(); // Lấy toàn bộ ChiTietHoaDon

            if (hoaDon.TrangThai == 1)
            {
                foreach (var chiTiet in dataCTHoaDon)
                {
                    var sanPham = await _context.SanPhams.FindAsync(chiTiet.SanPhamId);

                    if (sanPham != null)
                    {
                        sanPham.SoLuong -= chiTiet.SoLuong;

                        _context.SanPhams.Update(sanPham);
                    }
                }
              
            }
            _context.HoaDons.Update(hoaDon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalCount()
        {
            return await _context.HoaDons.CountAsync();
        }

        public IQueryable<HoaDon> GetAllHoaDons(DateTime? startDate, DateTime? endDate, int? trangThai, string? maHD)
        {
            var query = _context.HoaDons.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.CreatedDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.CreatedDate <= endDate.Value);

            if (trangThai.HasValue)
                query = query.Where(x => x.TrangThai == trangThai.Value);

            if (!string.IsNullOrEmpty(maHD))
                query = query.Where(x => x.MaHD.ToLower().Contains(maHD.ToLower()));

            return query.OrderByDescending(x => x.CreatedDate);
        }
        public async Task<List<ChiTietHoaDon?>> GetChiTietHoaDon(Guid idHoaDon)
        {
            var chiTietHoaDons = await _context.ChiTietHoaDons
            .Where(x => x.HoaDonId == idHoaDon)
            .Include(x => x.SanPham)
            .AsNoTracking()
            .ToListAsync();

            if (chiTietHoaDons == null || chiTietHoaDons.Count == 0)
            {
                return null;
            }

            return chiTietHoaDons;
        }


        public async Task CreateChiTietHoaDon(List<ChiTietHoaDon> chiTietHoaDon)
        {
            _context.ChiTietHoaDons.AddRange(chiTietHoaDon);
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
