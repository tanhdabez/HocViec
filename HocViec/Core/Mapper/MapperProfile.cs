using AutoMapper;
using Core.Request;
using Infrastructure.Models;

namespace Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User_Mapper
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>()
           .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
           .ForMember(dest => dest.Password, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Password)));

            //nhaCungCap_Mapper
            CreateMap<CreateNhaCungCapRequest, NhaCungCap>();
            CreateMap<NhaCungCapResponse, NhaCungCap>();
            CreateMap<NhaCungCap, NhaCungCapResponse>()
            .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            //loaiSanPham_Mapper
            CreateMap<CreateDanhMucLoaiHangRequest, DanhMucLoaiHang>();
            CreateMap<DanhMucLoaiHangResponse, DanhMucLoaiHang>();
            CreateMap<DanhMucLoaiHang, DanhMucLoaiHangResponse>()
           .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdatedDate));

            //sanPham_Mapper
            CreateMap<AddSanPhamRequest, SanPham>();
            CreateMap<SanPhamResponse, SanPham>()
            .ForMember(dest => dest.AnhSanPhams, opt => opt.Ignore());
            CreateMap<SanPham, SanPhamResponse>()
             .ForMember(dest => dest.TenNhaCungCap, opt => opt.MapFrom(src => src.NhaCungCap.Ten))
             .ForMember(dest => dest.TenDanhMucSanPham, opt => opt.MapFrom(src => src.DanhMucLoaiHang.Ten))
             .ForMember(dest => dest.AnhSanPhams, opt => opt.MapFrom(src => src.AnhSanPhams.Select(anh => anh.ImageUrl).ToList()))
             .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai))
             .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.Images, opt => opt.Ignore());

            //HoaDon_Mapper
            CreateMap<HoaDon, HoaDonResponse>();
            CreateMap<HoaDonResponse, HoaDon>();

        }
    }
}
