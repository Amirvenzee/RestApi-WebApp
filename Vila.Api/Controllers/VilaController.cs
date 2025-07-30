using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VILA.Api.Models;
using VILA.Api.Mappings;
using VILA.Api.Dtos;
using VILA.Api.Services.Vila;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;


namespace VILA.Api.Controllers
{
    
    [Route("api/v{version:apiVersion}/Vila")]
    
    [ApiController]
    public class VilaController : ControllerBase
    {
      
        private readonly IVilaService _vila;

        public VilaController(IVilaService vila)
        {
            _vila = vila;
        }
        /// <summary>
        /// دریافت لیست تمام ویلا ها 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
      
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VilaDto>))]
        public IActionResult GetAll()
        {
            var list = _vila.GetAll();

            return Ok(list);
        }

        #region Get data From Api
        //[HttpGet("GetDetails/{id:int}")] //OR   //[HttpGet("[action]/{id:int}")]  OR //[HttpGet("[action]/{id:int}/{Mobile:int}")]
        //                                                                                     
        //public IActionResult GetDetails(int id,int mobile )
        //{
        //    var Vila = _vila.GetById(id);
        //    if (Vila == null) return NotFound();

        //    return Ok(Vila.ToVilaDto());
        //}
        // https://localhost:44334/api/Vila/GetDetails/1/0916


        //[HttpGet("[action]")]
        //public IActionResult GetDetails([FromQuery] int id)
        //{
        //    var Vila = _vila.GetById(id);
        //    if (Vila == null) return NotFound();

        //    return Ok(Vila.ToVilaDto());
        //}
        //// https://localhost:44334/api/Vila/GetDetails?id=3


        //[HttpGet("[action]")]
        //public IActionResult GetDetails([FromHeader] int id)
        //{
        //    var Vila = _vila.GetById(id);
        //    if (Vila == null) return NotFound();

        //    return Ok(Vila.ToVilaDto());
        //}
        //// https://localhost:44334/api/Vila/GetDetails
        #endregion

        //User token
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJyb2xlIjoidXNlciIsIm5iZiI6MTc1Mzg3NDI2MCwiZXhwIjoxNzU0NDc5MDYwLCJpYXQiOjE3NTM4NzQyNjAsImlzcyI6IlZlbnplZSIsImF1ZCI6IndlYkFwaSJ9.i2GVxf93k_XndTpDRoiBZH1k_5LI6EfA5iJLp1rOWGg
        //
        //

        //Admin Token
        //
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3NTM4NzQ3MDEsImV4cCI6MTc1NDQ3OTUwMSwiaWF0IjoxNzUzODc0NzAxLCJpc3MiOiJWZW56ZWUiLCJhdWQiOiJ3ZWJBcGkifQ.tbLfQXU0iakziirT_pp6KBwaWYXKgD02FxpFNpXW7EY
        //

        /// <summary>
        /// دریافت یک ویلا با آی دی ویلا
        /// </summary>
        /// <param name="id"> آی دی ویلا</param>
        /// <returns></returns>
        /// 
        //میتونی فرام روت رو هم نزاری
        [HttpGet("[action]/{id:int}", Name = "GetDetails")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VilaDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDetails([FromRoute] int id)
        {
            var Vila = _vila.GetById(id);
            if (Vila == null) return NotFound();

           

            return Ok(Vila.ToDto());
        }
        
        /// <summary>
        /// ایجاد یک ویلای جدید
        /// </summary>
        /// <param name="model"> اطلاعات ویلا (VilaDto)</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(VilaDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]      
        public IActionResult Create([FromBody] VilaDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);            

            var vila = model.ToDataModel();

            if (_vila.Create(vila))
            {
                return CreatedAtRoute("GetDetails", new { model.Id },vila.ToDto() );
            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500,ModelState);
        }

        /// <summary>
        ///  ویرایش ویلا
        /// </summary>
        /// <param name="id"> آی دی ویلا</param>
        /// <param name="model"> اطلاعات ویلا (VilaDto)</param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update( int id,[FromBody] VilaDto model)
        {
            if(id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVila = model.ToDataModel();

            if (_vila.Update(newVila))
            {
                return NoContent();
              
            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500, ModelState);

            
        }


        /// <summary>
        /// حذف ویلا
        /// </summary>
        /// <param name="id">کلید ویلا</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]      
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
           var vila = _vila.GetById(id);

            if (vila == null)
            {
                return NotFound();
            }        

            if (_vila.Delete(vila))
            {
                return NoContent();
                
            }

            ModelState.AddModelError("", "مشکل از سمت سرور میباشد ... لطفا مجددا تلاش کنید ");

            return StatusCode(500, ModelState);

        }


    }
}
