using AIMS.Data.Contexts;
using AIMS.Data.Repositories.Interfaces;
using AIMS.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using AIMS.Service.Interfaces;
using AIMS.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ xác thực bằng Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn khi chưa đăng nhập
        options.LogoutPath = "/Account/Logout"; // Đường dẫn khi đăng xuất
        options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn khi bị từ chối truy cập
    });

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình các dịch vụ khác
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IWardRepository, WardRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IVnPayService, VnPayService>();

// Cấu hình chuỗi kết nối đến cơ sở dữ liệu
var connectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;Timeout=60;CommandTimeout=60;Connection Lifetime=60;Multiplexing=true";
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Cấu hình Razor View Engine
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Home/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Media/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Cart/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Payment/{0}" + RazorViewEngine.ViewExtension);
});

var app = builder.Build();

// Xử lý lỗi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm Middleware cho xác thực và ủy quyền
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Định tuyến
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
