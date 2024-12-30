﻿using AIMS.Data.Contexts;
using AIMS.Data.Repositories.Interfaces;
using AIMS.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IWardRepository, WardRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
var connectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;Timeout=60;CommandTimeout=60;Connection Lifetime=60;Multiplexing=true";
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear(); 
    options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Home/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Media/{0}" + RazorViewEngine.ViewExtension);  
    options.ViewLocationFormats.Add("/Views/Cart/{0}" + RazorViewEngine.ViewExtension);
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();