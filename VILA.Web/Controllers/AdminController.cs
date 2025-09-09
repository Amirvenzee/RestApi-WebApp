using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VILA.Web.Models.Detail;
using VILA.Web.Models.Vila;
using VILA.Web.Services.Customer;
using VILA.Web.Services.Detail;
using VILA.Web.Services.Vila;
using VILA.Web.Utility;

namespace VILA.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IVilaRepository _vila;
        private readonly IDetailRepository _detail;
        private readonly ApiUrls _apiurl;


        public AdminController(IAuthService auth, IVilaRepository vila, IOptions<ApiUrls> url, IDetailRepository detail)
        {
            _auth = auth;
            _vila = vila;
            _apiurl = url.Value;
            _detail = detail;
        }

        #region Vila


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

        #endregion

        public async Task<IActionResult> GetVilaDetails(int id)
        {
            var secret = _auth.GetJwtToken();
            string vilaUrl = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}/GetDetails/{id}";
            var vila = await _vila.GetById(vilaUrl, secret);
            if (vila == null) return NotFound();

            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/GetAllVilaDetails/{id}";
            var model = await _detail.GetAll(url, secret);
            ViewData["vila"] = vila;
            return View(model);
                      
        }
        public IActionResult CreateVilaDetail(int id)
        {
            DetailModel model = new DetailModel() {VilaId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVilaDetail(int id,DetailModel model)
        {
            if(id != model.VilaId) return BadRequest();
            if(!ModelState.IsValid)return View(model);

            var secret = _auth.GetJwtToken();
            string url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}";

            var create = await _detail.Create(url, secret, model);

            if (create)
            TempData["success"] = true;
                return Redirect($"/Admin/GetVilaDetails/{model.VilaId}");
         

        }
        
        public async Task<IActionResult> EditVilaDetail(int id)
        {
            var secret = _auth.GetJwtToken();
            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/GetById/{id}";

            var model = await _detail.GetById(url, secret);
            if(model == null) return NotFound();

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditVilaDetail(int id, DetailModel model)
        {
            if (id != model.DetailId) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var secret = _auth.GetJwtToken();
            string url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/{model.DetailId}";

            var Edit = await _detail.Update(url, secret, model);

            if (Edit)
                TempData["success"] = true;
            return Redirect($"/Admin/GetVilaDetails/{model.VilaId}");


        }

        public async Task<IActionResult> DeleteVilaDetail(int id)
        {
            var secret = _auth.GetJwtToken();
            string urlGet = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/GetById/{id}";
            var model = await _detail.GetById(urlGet, secret);
            

            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/{id}";
            bool delete = await _detail.Delete(url, secret);

            if(delete)
            TempData["success"] = true;

            return Redirect($"/Admin/GetVilaDetails/{model.VilaId}");

        }
    }
}
