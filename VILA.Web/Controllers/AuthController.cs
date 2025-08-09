using Microsoft.AspNetCore.Mvc;

namespace VILA.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
