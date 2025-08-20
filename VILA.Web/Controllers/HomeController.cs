using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VILA.Web.Models;
using VILA.Web.Services.Customer;
using VILA.Web.Services.Vila;

namespace VILA.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaRepository _vila;
        private readonly IAuthService _auth;

        public HomeController(IVilaRepository vila, IAuthService auth)
        {
            _vila = vila;
            _auth = auth;
        }

        public async Task<IActionResult> Index(int pageId = 1 ,string filter = "", int take = 6)
        {
            // string token = HttpContext.Session.GetString("JWTSecret");

            var token = _auth.GetJwtToken();
            var model = await _vila.Search(pageId,filter,take,token);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
