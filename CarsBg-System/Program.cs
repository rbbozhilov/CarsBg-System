using CarsBg_System.Data;
using CarsBg_System.Data.Models;
using CarsBg_System.Infrastructure;
using CarsBg_System.Services.Brand;
using CarsBg_System.Services.Car;
using CarsBg_System.Services.Category;
using CarsBg_System.Services.Engine;
using CarsBg_System.Services.Extra;
using CarsBg_System.Services.Model;
using CarsBg_System.Services.Region;
using CarsBg_System.Services.Status;
using CarsBg_System.Services.Transmission;
using CarsBg_System.Services.WheelDrive;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CarsDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarsDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IWheelDriveService, WheelDriveService>();
builder.Services.AddTransient<IEngineService, EngineService>();
builder.Services.AddTransient<ITransmissionService, TransmissionService>();
builder.Services.AddTransient<IRegionService, RegionService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<IExtraService, ExtraService>();

var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
