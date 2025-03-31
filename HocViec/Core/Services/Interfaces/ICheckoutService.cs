using Core.Request;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<GioHangDto> GetSanPhamsByIdsAsync(List<CheckOutDetailsDto> cartItems);
        Task<bool> CreateHoaDonAsync(CheckOutRequest request, Guid? userId);
      
    }
}
