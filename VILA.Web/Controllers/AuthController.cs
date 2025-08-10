using Microsoft.AspNetCore.Mvc;
using VILA.Web.Models.Customer;

namespace VILA.Web.Controllers
{
    public class AuthController : Controller
    {
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
            return View();
        }
    }
}
