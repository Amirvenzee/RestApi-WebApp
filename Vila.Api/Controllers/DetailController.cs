using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VILA.Api.Dtos;
using VILA.Api.Mappings;
using VILA.Api.Services.Detail;
using VILA.Api.Services.Vila;

namespace VILA.Api.Controllers
{

   
    [Route("api/v{version:apiVersion}/Detail")]
    //[Route("api/detail")] $$Take Url From Header or Query
    //[ApiVersion("1.0")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detail;
        private readonly IVilaService _vila;


        public DetailController(IVilaService vila, IDetailService detail)
        {
            _vila = vila;
            _detail = detail;
        }


        /// <summary>
        /// دریافت تمام جزییات ویلا
        /// </summary>
        /// <param name="vilaId">آی دی ویلا</param>
        /// <returns></returns>
        [HttpGet("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<DetailDto>))]
        [ProducesResponseType(404)]
      
        public IActionResult GetAllVilaDetails(int vilaId)
        {
            var vila = _vila.GetById(vilaId);

            if(vila == null)            
                return NotFound();
            
            var details = _detail.GetAllVilaDetails(vilaId);
            

            return Ok(details);
        }


        /// <summary>
        /// دریافت یک جز از ویلا
        /// </summary>
        /// <param name="detailId"> آی دی جز</param>
        /// <returns></returns>
        [HttpGet("[action]/{detailId:int}",Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailDto))]
        [ProducesResponseType(404)]
      
        public IActionResult GetById(int detailId)
        {
            var detail = _detail.getById(detailId);
            if (detail == null)
                return NotFound();
            return Ok(detail.ToDto());
        }


        /// <summary>
        /// افزدون جزییات جدید به ویلا
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type =typeof(DetailDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
       
        public IActionResult Create([FromBody] DetailDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = model.ToDataModel();

            if (_detail.Create(detail))
            {
                
                return CreatedAtRoute("GetById", new { DetailId = detail.Id }, detail.ToDto());
            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500, ModelState);
        }

        /// <summary>
        /// ویرایش جز ویلا
        /// </summary>
        /// <param name="detailId"> آی دی جز</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{detailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Update(int detailId, [FromBody] DetailDto model)
        {
            if (detailId != model.DetailId)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVila = model.ToDataModel();

            if (_detail.Update(newVila))
            {
                return NoContent();

            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500, ModelState);


        }

        /// <summary>
        /// حذف جز ویلا
        /// </summary>
        /// <param name="detailId"> آی دی جز</param>
        /// <returns></returns>
        [HttpDelete("{detailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
 
        public IActionResult Delete(int detailId)
        {
            var detail = _detail.getById(detailId);

            if (detail == null)
            {
                return NotFound();
            }

            if (_detail.Delete(detail))
            {
                return NoContent();

            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500, ModelState);

        }
    }
}
