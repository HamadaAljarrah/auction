using DistLab2;
using DistLab2.Core;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//db med dependency injection Marcus
 builder.Services.AddDbContext<AuctionDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));


//builder.Services.AddScoped<IAuctionService, AuctionService>();


// Add DbContext registration Hamada
//builder.Services.AddDbContext<AuctionDbContext>(options => options.UseSqlite("Data Source=auctions.db"));
//builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlite("Data Source=users.db"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IReposetory<AuctionDb>, Reposetory<AuctionDb>>();
builder.Services.AddScoped<IReposetory<BidDb>, Reposetory<BidDb>>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<UserManager<IdentityUser>>();


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

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
