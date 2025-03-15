using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class DanhMucLoaiHangService : IDanhMucLoaiHangService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DanhMucLoaiHang> _danhMucLoaiHang;
        private readonly IRepository<SanPham> _sanPhamRepo;
        private readonly AppDbContext _dbContext;
        public DanhMucLoaiHangService(IMapper mapper, AppDbContext dbContext, IRepository<DanhMucLoaiHang> danhMucLoaiHang, IRepository<SanPham> sanPhamRepo)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _danhMucLoaiHang = danhMucLoaiHang;
            _sanPhamRepo = sanPhamRepo;
        }

        public async Task<List<DanhMucLoaiHangResponse>> GetAllDanhMucLoaiHang()
        {
            var response = await _danhMucLoaiHang.GetAllAsync();
            return _mapper.Map<List<DanhMucLoaiHangResponse>>(response);
        }

        public async Task<DanhMucLoaiHangResponse> GetDanhMucLoaiHangById(Guid id)
        {
            var response = await _danhMucLoaiHang.GetByIdAsync(id);
            if (response == null)
            {
                throw new KeyNotFoundException("Không tìm thấy nhà cung cấp");
            }
            return _mapper.Map<DanhMucLoaiHangResponse>(response);
        }

        public async Task<bool> AddDanhMucLoaiHang(CreateDanhMucLoaiHangRequest request)
        {
            var newDanhMucLoaiHang = _mapper.Map<DanhMucLoaiHang>(request);
            await _danhMucLoaiHang.AddAsync(newDanhMucLoaiHang);
            return true;
        }

        public async Task<DanhMucLoaiHangResponse?> UpdateDanhMucLoaiHang(DanhMucLoaiHangResponse request)
        {
            var response = await _dbContext.DanhMucLoaiHangs.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (response != null)
            {
                _mapper.Map(request, response);
                await _danhMucLoaiHang.UpdateAsync(response);
            }
            return _mapper.Map<DanhMucLoaiHangResponse?>(response);
        }

        public async Task<bool> UpdateStatusDanhMucLoaiHang(Guid id)
        {
            await _danhMucLoaiHang.UpdateStatusAsync(id);
            var danhMucLoaiHang = await _dbContext.DanhMucLoaiHangs.FindAsync(id);
            var dataSanPham = await _dbContext.SanPhams.Where(x => x.IdDanhMucSanPham == id).Select(x => new { x.Id, x.IdNhaCungCap, x.TrangThai }).ToListAsync();
            if (dataSanPham.Any())
            {
                var nhaCungCapIds = dataSanPham.Select(x => x.IdNhaCungCap).Distinct().ToList();
                var nhaCungCapDict = await _dbContext.NhaCungCaps
                    .Where(x => nhaCungCapIds.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id, x => x.TrangThai);
                foreach (var item in dataSanPham)
                {
                    if (danhMucLoaiHang != null && danhMucLoaiHang.TrangThai) // Nếu danh mục bật
                    {
                        if (nhaCungCapDict.TryGetValue(item.IdNhaCungCap, out bool nhaCungCapTrangThai))
                        {
                            if (nhaCungCapTrangThai) // Nếu nhà cung cấp bật
                            {
                                await _sanPhamRepo.UpdateStatusAsync(item.Id); // Bật sản phẩm
                            }
                            else // Nếu nhà cung cấp tắt
                            {
                                if (item.TrangThai)
                                {
                                    await _sanPhamRepo.UpdateStatusAsync(item.Id); // Tắt sản phẩm nếu đang bật
                                }
                            }
                        }
                    }
                    else // Nếu danh mục tắt
                    {
                        if (item.TrangThai)
                        {
                            await _sanPhamRepo.UpdateStatusAsync(item.Id); // Tắt sản phẩm nếu đang bật
                        }
                    }
                }
            }
            return true;
        }

    }
}
