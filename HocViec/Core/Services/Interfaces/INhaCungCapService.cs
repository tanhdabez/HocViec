using Core.Request;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCapResponse>> GetAllNhaCungCap();
        Task<NhaCungCapResponse> GetNhaCungCapById(Guid id);

        Task<bool> AddNhaCungCap(CreateNhaCungCapRequest request);

        Task<NhaCungCapResponse?> UpdateNhaCungCap(NhaCungCapResponse request);

        Task<bool> UpdateStatusNhaCungCap(Guid id);
    }
}
