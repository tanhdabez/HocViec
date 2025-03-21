using AutoMapper;
using Azure.Core;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;

namespace Core.Services.Implements
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;
        public CheckoutService(ICheckoutRepository checkoutRepository, IMapper mapper)
        {
            _checkoutRepository = checkoutRepository;
            _mapper = mapper;
        }
        public async Task<bool> CheckoutAsync(CheckoutRequest request)
        {
           var hoaDon = _mapper.Map<HoaDon>(request);
            await _checkoutRepository.CheckoutAsync(hoaDon);
            return true;
        }

        public async Task<CheckoutRequest?> GetHoaDonById(Guid Id)
        {
            var hoaDon = await _checkoutRepository.GetHoaDonById(Id);
            return _mapper.Map<CheckoutRequest>(hoaDon);
        }
    }
}
