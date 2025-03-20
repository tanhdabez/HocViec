using Microsoft.EntityFrameworkCore;
using System;
using Core.Constants;
using Infrastructure;
using Core.Mapper;
using Core.Services.Implements;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories.Implements;

var builder = WebApplication.CreateBuilder(args);
var appSettingsSection = builder.Configuration.GetSection("Appsettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm dịch vụ xác thực (Authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Đường dẫn đến trang đăng nhập
        options.AccessDeniedPath = "/Index"; // Đường dẫn đến trang từ chối truy cập
    });

builder.Services.AddAuthorization();
//db config & migrate - start
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(appSettings.ConnectionString));

//services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<DbContext, AppDbContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<INhaCungCapService, NhaCungCapService>();
builder.Services.AddScoped<INhaCungCapRepository, NhaCungCapRepository>();

builder.Services.AddScoped<IDanhMucLoaiHangService, DanhMucLoaiHangService>();
builder.Services.AddScoped<IDanhMucLoaiHangRepository, DanhMucLoaiHangRepository>();

builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();

builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
