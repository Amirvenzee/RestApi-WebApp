using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using VILA.Web.Models.Vila;
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

        public IActionResult AddVila()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVila(VilaModel vila, IFormFile picture)
        {
            if (!ModelState.IsValid) return View(vila);

            try
            {
                DateTime date = vila.BuildDate.ToEnglishDateTime();
            }
            catch
            {
                ModelState.AddModelError("BuildDate", "فرمت ناریخ باید 1365/04/01 باشد.");
                return View(vila);
            }

            if (picture == null || !picture.IsImage())
            {
                ModelState.AddModelError("Image", "لطفا عکس با فرمت jpg واردکنید .");
                return View(vila);
            }

            //convert picture to byte[]

            using (var open = picture.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                open.CopyTo(ms);
                vila.Image = ms.ToArray();
            }


            var secret = _auth.GetJwtToken();
            var url = _apiurl.BaseAddress + _apiurl.VilaV1Address;

            bool create = await _vila.Create(url, secret, vila);

            if (create)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }

        public async Task<IActionResult> EditVila(int id)
        {
            var seccret = _auth.GetJwtToken();
            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}/GetDetails/{id}";
            var model = await _vila.GetById(url, seccret);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditVila(int id,VilaModel vila, IFormFile? picture)
        {
            if (!ModelState.IsValid) return View(vila);
           if(picture != null)
            {
                if (!picture.IsImage())
                {
                    ModelState.AddModelError("Image", "لطفا عکس با فرمت jpg واردکنید .");
                    return View(vila);
                }
            }
            //convert picture to byte[]

            using (var open = picture.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                open.CopyTo(ms);
                vila.Image = ms.ToArray();
            }


            var secret = _auth.GetJwtToken();
            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}/{id}";

            bool update = await _vila.Update(url, secret, vila);

            if (update)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }

        public async Task<IActionResult> DeleteVila(int id)
        {
            var secret = _auth.GetJwtToken();
            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}/{id}";

            bool delete = await _vila.Delete(url, secret);

            if (delete)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }
    }
}
