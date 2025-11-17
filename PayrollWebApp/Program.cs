using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayrollWebApp.Controllers;
using PayrollWebApp.Services;
using PayrollWebApp.Data;


var builder = WebApplication.CreateBuilder(args);

// Base URL can change the port in launchSettings.json
var apiBase = builder.Configuration[Const.API_BASE_URL];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IEmployeeApiService, EmployeeApiService>(client => client.BaseAddress = new Uri(apiBase));
builder.Services.AddHttpClient<IPayslipApiService, PayslipApiService>(client => client.BaseAddress = new Uri(apiBase));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
