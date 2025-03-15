using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IRepository<NhaCungCap> _nhaCungCapRepo;
        private readonly IRepository<SanPham> _sanPhamRepo;
        public NhaCungCapService(IMapper mapper, AppDbContext dbContext, IRepository<NhaCungCap> nhaCungCapRepo, IRepository<SanPham> sanPhamRepo)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _nhaCungCapRepo = nhaCungCapRepo;
            _sanPhamRepo = sanPhamRepo;
        }

        public async Task<List<NhaCungCapResponse>> GetAllNhaCungCap()
        {
            var nhaCungCaps = await _nhaCungCapRepo.GetAllAsync();
            return _mapper.Map<List<NhaCungCapResponse>>(nhaCungCaps);
        }

        public async Task<NhaCungCapResponse> GetNhaCungCapById(Guid id)
        {
            var NhaCungCap = await _nhaCungCapRepo.GetByIdAsync(id);
            if (NhaCungCap == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy nhà cung cấp với ID: {id}");
            }
            return _mapper.Map<NhaCungCapResponse>(NhaCungCap);
        }

        public async Task<bool> AddNhaCungCap(CreateNhaCungCapRequest request)
        {
            var newNhaCungCap = _mapper.Map<NhaCungCap>(request);
            await _nhaCungCapRepo.AddAsync(newNhaCungCap);
            return true;
        }

        public async Task<NhaCungCapResponse?> UpdateNhaCungCap(NhaCungCapResponse request)
        {
            //var response = await _dbContext.NhaCungCaps.FirstOrDefaultAsync(x => x.Id == request.Id);
            //if (response != null)
            //{
            //    _mapper.Map(request, response);
            //    await _nhaCungCapRepo.UpdateAsync(response);
            //}
            //return _mapper.Map<NhaCungCapResponse>(response);

            var response = await _dbContext.NhaCungCaps.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (response != null)
            {
                _mapper.Map(request, response);
                await _nhaCungCapRepo.UpdateAsync(response);
                return _mapper.Map<NhaCungCapResponse>(response);
            }
            return null;
        }

        public async Task<bool> UpdateStatusNhaCungCap(Guid id)
        {
            await _nhaCungCapRepo.UpdateStatusAsync(id);
            var nhaCungCap = await _dbContext.NhaCungCaps.FindAsync(id);
            var dataSP = await _dbContext.SanPhams.Where(x => x.IdNhaCungCap == id).Select(x => new { x.Id, x.TrangThai, x.IdDanhMucSanPham }).ToListAsync();
            if (dataSP.Any())
            {
                var danhMucIds = dataSP.Select(sp => sp.IdDanhMucSanPham).Distinct().ToList(); // Sửa IdNhaCungCap thành IdDanhMucSanPham
                var danhMucSanPhams = await _dbContext.DanhMucLoaiHangs
                    .Where(dm => danhMucIds.Contains(dm.Id))
                    .ToDictionaryAsync(dm => dm.Id, dm => dm.TrangThai);
                foreach (var item in dataSP)
                {
                    if (danhMucSanPhams.TryGetValue(item.IdDanhMucSanPham, out bool trangThaiDanhMuc)) // Sửa IdNhaCungCap thành IdDanhMucSanPham
                    {
                        if (nhaCungCap != null && nhaCungCap.TrangThai && trangThaiDanhMuc) // Kiểm tra cả nhà cung cấp và danh mục
                        {
                            await _sanPhamRepo.UpdateStatusAsync(item.Id); // Bật sản phẩm nếu cả hai đều bật
                        }
                        else if (item.TrangThai)
                        {
                            await _sanPhamRepo.UpdateStatusAsync(item.Id); // Tắt sản phẩm nếu đang bật
                        }
                    }
                    else if (item.TrangThai)
                    {
                        await _sanPhamRepo.UpdateStatusAsync(item.Id); // Tắt sản phẩm nếu đang bật
                    }
                }
            }
            return true;
        }
    }
}
