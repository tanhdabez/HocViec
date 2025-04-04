using Core.Request;
using Core.Response;

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
