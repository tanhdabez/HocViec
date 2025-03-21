using Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<bool> CheckoutAsync(CheckoutRequest request);
        Task<CheckoutRequest?> GetHoaDonById(Guid Id);
    }
}
