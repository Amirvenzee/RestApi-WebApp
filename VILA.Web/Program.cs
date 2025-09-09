using Microsoft.AspNetCore.Authentication.Cookies;
using VILA.Web.Services.Customer;
using VILA.Web.Services.Detail;
using VILA.Web.Services.Vila;
using VILA.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();
services.AddCors();
services.AddHttpClient();

#region Auth
services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(x =>
    {
        x.Cookie.HttpOnly = true;
        x.ExpireTimeSpan = TimeSpan.FromDays(7);
        x.LoginPath = "/Auth/Login";
        x.LogoutPath = "/Auth/Logout";
        x.AccessDeniedPath = "/Auth/NotAccess";
    });
services.AddHttpContextAccessor();
#endregion

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
services.AddTransient<IVilaRepository, VilaRepository>();
services.AddTransient<IDetailRepository, DetailRepository>();
services.AddTransient<IAuthService, AuthService>();
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
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseRouting();
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
