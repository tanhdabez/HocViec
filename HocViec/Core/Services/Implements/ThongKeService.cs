using Core.Response;
using Core.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;


namespace Core.Services.Implements
{
    public class ThongKeService : IThongKeService
    {
        private readonly IThongKeRepository _thongKeRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;

        public ThongKeService(IThongKeRepository thongKeRepository, INhaCungCapRepository nhaCungCapRepository)
        {
            _thongKeRepository = thongKeRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
        }

        public async Task<ThongKeResponse> GetThongKeAsync(DateTime startDate, DateTime endDate)
        {
            var nhaCungCaps = await _nhaCungCapRepository.GetAllAsync();
            var soLuongBanRa = await _thongKeRepository.GetSoLuongHangHoaBanAsync(startDate, endDate);
            var soLuongConLai = await _thongKeRepository.GetSoLuongHangHoaConLaiAsync();
            var soLuongHoaDon = await _thongKeRepository.GetSoLuongHoaDonBanAsync(startDate, endDate);

            var topSoLuong = new List<(Guid NhaCungCapId, int SoLuong)>();

            foreach (var items in nhaCungCaps)
            {
                var soLuong = await _thongKeRepository.GetTopSoLuongBanRaAsync(items.Id, startDate, endDate);
                topSoLuong.Add((items.Id, soLuong));
            }

            var topNhaCungCap = topSoLuong.OrderByDescending(x => x.SoLuong).Take(10).ToList();

            var topNhaCungCapNames = topNhaCungCap.Select(x => nhaCungCaps.FirstOrDefault(n => n.Id == x.NhaCungCapId)?.Ten ?? "Không có nhà cung cấp").ToList();

            var topSoLuongList = topNhaCungCap.Select(x => x.SoLuong).ToList();

            var response = new ThongKeResponse
            {
                StartDate = startDate,
                EndDate = endDate,
                SoLuongHangBan = soLuongBanRa,
                SoLuongHangConLai = soLuongConLai,
                SoLuongHoaDon = soLuongHoaDon,
                TopSoLuong = topSoLuongList,
                TopNhaCungCap = topNhaCungCapNames
            };

            return response;
        }

    }
}
