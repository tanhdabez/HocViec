using Core.Response;

namespace Core.Services.Interfaces
{
    public interface IThongKeService
    {
        Task<ThongKeResponse> GetThongKeAsync(DateTime startDate, DateTime endDate);
    }

}
