using VILA.Web.Services.Customer;
using VILA.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
