using DistLab2;
using DistLab2.Core;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//db med dependency injection
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IReposetory<AuctionDb>, Reposetory<AuctionDb>>();
builder.Services.AddScoped<IReposetory<BidDb>, Reposetory<BidDb>>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//builder.Services.AddDbContext<AuctionDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("AuctionDbConnection")));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
