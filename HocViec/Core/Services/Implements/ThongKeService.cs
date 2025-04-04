using Core.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;


namespace Core.Services.Implements
{
    public class ThongKeService : IThongKeService
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeService(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        public async Task<int> GetSoLuongHangHoaBanAsync(DateTime startDate, DateTime endDate)
        {
            return await _thongKeRepository.GetSoLuongHangHoaBanAsync(startDate, endDate);
        }

        public async Task<int> GetSoLuongHangHoaConLaiAsync()
        {
            return await _thongKeRepository.GetSoLuongHangHoaConLaiAsync();
        }

        public async Task<int> GetSoLuongHoaDonBanAsync(DateTime startDate, DateTime endDate)
        {
            return await _thongKeRepository.GetSoLuongHoaDonBanAsync(startDate, endDate);
        }
    }
}
