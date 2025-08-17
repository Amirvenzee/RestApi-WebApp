using Microsoft.AspNetCore.Authentication.Cookies;
using VILA.Web.Services.Customer;
using VILA.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();
services.AddHttpClient();



#region Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion

#region Dependency
services.AddTransient<ICustomerRepository, CustomerRepository>();
#endregion

#region ApiUrls
var apiUrlsSection = builder.Configuration.GetSection("ApiUrls");
services.Configure<ApiUrls>(apiUrlsSection);
#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
