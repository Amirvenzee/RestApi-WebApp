using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using VILA.Web.Models.Customer;
using VILA.Web.Services.Customer;

namespace VILA.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerRepository _customer;

        public AuthController(ICustomerRepository customer)
        {
            _customer = customer;
        }

        //Comment1
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
                 
              var res =  await _customer.Register(model);
            if(res.Result)
            {
                TempData["success"] = true;
               return View();
            }
            ModelState.AddModelError("", res.Message);
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var res = await _customer.Login(model);
            if (!res.Result.Result)
            {

                ModelState.AddModelError("", res.Result.Message);
                return View(model);
              
            }
            var customer = res.Customer;

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, customer.Mobile));
            identity.AddClaim(new Claim(ClaimTypes.Role, customer.Role));
            identity.AddClaim(new Claim("JWTSecret", customer.JwtSecret));



            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect("/");
        }

    }
}
