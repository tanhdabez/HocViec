using Core.Request;
using Core.Response;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<GioHangDto> GetSanPhamsByIdsAsync(List<CheckOutDetailsResponse> cartItems);
        Task<bool> CreateHoaDonAsync(CheckOutRequest request, Guid? userId);
      
    }
}
