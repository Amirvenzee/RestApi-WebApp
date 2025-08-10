using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
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
            {
                return View(model);
            }
          var res =  await _customer.Register(model);
            if(res.result)
            {
               return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
