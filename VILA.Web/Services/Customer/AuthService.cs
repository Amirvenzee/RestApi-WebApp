using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace VILA.Web.Services.Customer
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _context;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor;
        }

        public string GetJwtToken()
        {
           var claims = _context.HttpContext.User.Claims.ToList();
            if (claims.Count() < 1) return "";
            return _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "JWTSecret").Value;
        }

        public void Logout()
        {
            _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
