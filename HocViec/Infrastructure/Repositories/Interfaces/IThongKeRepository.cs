namespace Infrastructure.Repositories.Interfaces
{
    public interface IThongKeRepository
    {
        Task<int> GetSoLuongHangHoaBanAsync(DateTime month, DateTime endDate);
        Task<int> GetSoLuongHangHoaConLaiAsync();
        Task<int> GetSoLuongHoaDonBanAsync(DateTime startDate, DateTime endDate);
    }
}
