using Core.Request;
using Infrastructure.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IDanhMucLoaiHangService
    {
        Task<List<DanhMucLoaiHangResponse>> GetAllDanhMucLoaiHang();
        Task<DanhMucLoaiHangResponse> GetDanhMucLoaiHangById(Guid id);

        Task<bool> AddDanhMucLoaiHang(CreateDanhMucLoaiHangRequest request);

        Task<DanhMucLoaiHangResponse?> UpdateDanhMucLoaiHang(DanhMucLoaiHangResponse request);

        Task<bool> UpdateStatusDanhMucLoaiHang(Guid id);
    }
}
