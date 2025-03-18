using Core.Request;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ISanPhamService
    {
        Task<List<SanPhamResponse>> GetAllSanPham();
        Task<SanPhamResponse> GetSanPhamById(Guid id);

        Task<bool> AddSanPham(AddSanPhamRequest request);

        Task<SanPhamResponse?> UpdateSanPham(SanPhamResponse request);

        Task<bool> DeleteSanPham(Guid id);

        Task<bool> UpdateStatus(Guid id);

        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
