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
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly INhaCungCapRepository _nhaCungCapRepo;
        public NhaCungCapService(IMapper mapper, AppDbContext dbContext, INhaCungCapRepository nhaCungCapRepo)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _nhaCungCapRepo = nhaCungCapRepo;
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

        public async Task<Dictionary<int, int>> GetMonthlySalesBySupplierId(Guid id)
        {
            return await _nhaCungCapRepo.GetMonthlySalesBySupplierIdAsync(id);
        }

        public async Task<bool> AddNhaCungCap(CreateNhaCungCapRequest request)
        {
            var newNhaCungCap = _mapper.Map<NhaCungCap>(request);
            await _nhaCungCapRepo.AddAsync(newNhaCungCap);
            return true;
        }

        public async Task<NhaCungCapResponse?> UpdateNhaCungCap(NhaCungCapResponse request)
        {
            var response = await _nhaCungCapRepo.GetByIdAsync(request.Id);
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
            return await _nhaCungCapRepo.UpdateStatusAsync(id);
        }
    }
}
