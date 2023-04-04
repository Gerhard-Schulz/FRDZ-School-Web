using FRDZSchool.DataAccess.Data;
using FRDZSchool.DataAccess.Repository;
using FRDZSchool.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
//    "DefaultConnection", b => b.MigrationsAssembly("FRDZ School Web")));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
    pattern: "{area=Visitor}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "parametrs",
//    pattern: "{controller}/{action}/{x:int}/{y:int}");

//app.MapControllerRoute(
//    name: "CatchAll",
//    pattern: "{controller=Values}/{action=Mult}/{*catchall}");

app.Run();
