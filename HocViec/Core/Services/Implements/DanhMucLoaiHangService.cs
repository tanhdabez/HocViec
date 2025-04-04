using AutoMapper;
using Core.Request;
using Core.Response;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class DanhMucLoaiHangService : IDanhMucLoaiHangService
    {
        private readonly IMapper _mapper;
        private readonly IDanhMucLoaiHangRepository _danhMucLoaiHangRepo;
        private readonly AppDbContext _dbContext;
        public DanhMucLoaiHangService(IMapper mapper, AppDbContext dbContext, IDanhMucLoaiHangRepository danhMucLoaiHangRepo)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _danhMucLoaiHangRepo = danhMucLoaiHangRepo;
        }

        public async Task<List<DanhMucLoaiHangResponse>> GetAllDanhMucLoaiHang()
        {
            var response = await _danhMucLoaiHangRepo.GetAllAsync();
            return _mapper.Map<List<DanhMucLoaiHangResponse>>(response);
        }

        public async Task<DanhMucLoaiHangResponse> GetDanhMucLoaiHangById(Guid id)
        {
            var response = await _danhMucLoaiHangRepo.GetByIdAsync(id);
            if (response == null)
            {
                throw new KeyNotFoundException("Không tìm thấy nhà cung cấp");
            }
            return _mapper.Map<DanhMucLoaiHangResponse>(response);
        }

        public async Task<bool> AddDanhMucLoaiHang(CreateDanhMucLoaiHangRequest request)
        {
            var newDanhMucLoaiHang = _mapper.Map<DanhMucLoaiHang>(request);
            await _danhMucLoaiHangRepo.AddAsync(newDanhMucLoaiHang);
            return true;
        }

        public async Task<DanhMucLoaiHangResponse?> UpdateDanhMucLoaiHang(DanhMucLoaiHangResponse request)
        {
            var response = await _dbContext.DanhMucLoaiHangs.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (response != null)
            {
                _mapper.Map(request, response);
                await _danhMucLoaiHangRepo.UpdateAsync(response);
            }
            return _mapper.Map<DanhMucLoaiHangResponse?>(response);
        }

        public async Task<bool> UpdateStatusDanhMucLoaiHang(Guid id)
        {
           return await _danhMucLoaiHangRepo.UpdateStatusAsync(id);
        }

    }
}
