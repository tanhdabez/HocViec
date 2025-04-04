using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Authentication & Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; 
        options.AccessDeniedPath = "/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
//db config & migrate - start
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(appSettings.ConnectionString));

//services
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

builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();

builder.Services.AddScoped<IHoaDonService, HoaDonService>();

builder.Services.AddScoped<IThongKeService, ThongKeService>();
builder.Services.AddScoped<IThongKeRepository, ThongKeRepository>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
