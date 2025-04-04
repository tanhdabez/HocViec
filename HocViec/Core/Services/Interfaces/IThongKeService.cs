namespace Core.Services.Interfaces
{
    public interface IThongKeService
    {
        Task<int> GetSoLuongHangHoaBanAsync(DateTime startDate, DateTime endDate);
        Task<int> GetSoLuongHangHoaConLaiAsync();
        Task<int> GetSoLuongHoaDonBanAsync(DateTime startDate, DateTime endDate);
    }

}
