using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class CommonResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic? Data { get; set; }
        public int? TotalRecord { get; set; }

        // Constructor mặc định (nếu không truyền tham số, dùng giá trị mặc định)
        public CommonResponse()
        {
            Status = "error";
            Message = "Không có kết nối! Vui lòng kiểm tra đường truyền!";
        }

        // Constructor có tham số để tạo response tùy chỉnh
        public CommonResponse(string status, string message, dynamic? data = null, int? totalRecord = 0)
        {
            Status = status;
            Message = message;
            Data = data;
            TotalRecord = totalRecord;
        }
    }
}
