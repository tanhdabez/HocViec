namespace Core.Request
{
    public class FilterRequest
    {
        public int Page { get; set; } = 1; // Mặc định trang đầu tiên
        public int PageSize { get; set; } = 8; // Số lượng item mỗi trang
        public string? StartDate { get; set; } // Ngày bắt đầu
        public string? EndDate { get; set; } // Ngày kết thúc
        public int? Status { get; set; } // Trạng thái (VD: hóa đơn, sản phẩm, user)
        public string? SearchKeyword { get; set; } // Tìm kiếm chung (VD: tên sản phẩm, tên user)
        public string? SortBy { get; set; } // Sắp xếp theo trường nào
        public bool IsDescending { get; set; } = true; // Sắp xếp giảm dần hay tăng dần
    }

}
