using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICheckoutRepository
    {
        
        //Checkout
        public Task<bool> CheckoutAsync(HoaDon request);
        public Task<HoaDon?> GetHoaDonById(Guid Id);
        string GenerateInvoiceCode(string prefix);
    }
}
