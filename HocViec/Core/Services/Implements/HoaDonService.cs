using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;

namespace Core.Services.Implements
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IMapper _mapper;
        public HoaDonService(IHoaDonRepository hoaDonRepository, IMapper mapper, ISanPhamRepository sanPhamRepository)
        {
            _hoaDonRepository = hoaDonRepository;
            _mapper = mapper;
            _sanPhamRepository = sanPhamRepository;
        }
        public async Task<List<HoaDonResponse>> GetAllHoaDonAsync(DateTime? startDate, DateTime? endDate, int? trangThai)
        {
            var hoaDons = await _hoaDonRepository.GetAllHoaDons(startDate, endDate, trangThai);
            return _mapper.Map<List<HoaDonResponse>>(hoaDons);
        }

        public async Task<HoaDon> UpdateHoaDonAsync(HoaDon hoaDon)
        {
            return await _hoaDonRepository.UpdateHoaDon(hoaDon);
        }


        public async Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonIdAsync(Guid hoaDonId)
        {
            return await _hoaDonRepository.GetChiTietHoaDonsByHoaDonId(hoaDonId);
        }
    }
}
