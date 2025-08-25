using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using VILA.Web.Services.Customer;
using VILA.Web.Services.Vila;
using VILA.Web.Utility;

namespace VILA.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IVilaRepository _vila;
        private readonly ApiUrls _apiurl;


        public AdminController(IAuthService auth, IVilaRepository vila,IOptions<ApiUrls> url)
        {
            _auth = auth;
            _vila = vila;
            _apiurl = url.Value;
        }


        public async Task<IActionResult> AllVilas()
        {
            var seccret = _auth.GetJwtToken();
            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}";
            var model = await _vila.GetAll(url, seccret);
            return View(model);
        }
    }
}
