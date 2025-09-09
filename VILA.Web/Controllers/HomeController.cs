using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using VILA.Web.Models;
using VILA.Web.Models.Vila;
using VILA.Web.Services.Customer;
using VILA.Web.Services.Detail;
using VILA.Web.Services.Vila;
using VILA.Web.Utility;

namespace VILA.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaRepository _vila;
        private readonly IDetailRepository _detail;
        private readonly IAuthService _auth;
        private readonly ApiUrls _apiurl;

        public HomeController(IVilaRepository vila, IAuthService auth, IOptions<ApiUrls> apiurl, IDetailRepository detail)
        {
            _vila = vila;
            _auth = auth;
            _apiurl = apiurl.Value;
            _detail = detail;
        }

        [Authorize]
        public async Task<IActionResult> Index(int pageId = 1 ,string filter = "", int take = 6)
        {
            // string token = HttpContext.Session.GetString("JWTSecret");

            var token = _auth.GetJwtToken();
            var model = await _vila.Search(pageId,filter,take,token);
            return View(model);
        }

      
        public async Task<IActionResult> VilaPage(int id)
        {
            var token = _auth.GetJwtToken();

           var vilaUrl = $"{_apiurl.BaseAddress}{_apiurl.VilaV1Address}/GetDetails/{id}";
            var VilaModel =await _vila.GetById(vilaUrl, token);

            var url = $"{_apiurl.BaseAddress}{_apiurl.VilaDetailAddress}/GetAllVilaDetails/{id}";
            var detailModel = await _detail.GetAll(url, token);

            var model = VilaDetail.OneVilaDetail(VilaModel, detailModel);

            
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
