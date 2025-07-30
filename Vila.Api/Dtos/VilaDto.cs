using System.ComponentModel.DataAnnotations;
using VILA.Api.ModelValidation;

namespace VILA.Api.Dtos
{
    public class VilaDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="نام ویلا اجباری است ")]
        [MaxLength(255,ErrorMessage = "نام ویلا نباید بیش از 255 حرف باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = " استان ویلا اجباری است ")]
        [MaxLength(255, ErrorMessage = "نام استان ویلا نباید بیش از 255 حرف باشد")]
        public string State { get; set; }

        [Required(ErrorMessage = " شهرستان ویلا اجباری است ")]
        [MaxLength(255, ErrorMessage = "نام شهر ویلا نباید بیش از 255 حرف باشد")]
        public string City { get; set; }

        [Required(ErrorMessage = "آدرس کامل ویلا اجباری است ")]
        [MaxLength(500, ErrorMessage = "آدرس کامل ویلا نباید بیش از 500 حرف باشد")]
        public string Address { get; set; }

        [Required(ErrorMessage = "شماره تلفن ویلا اجباری است ")]
        [MaxLength(11, ErrorMessage = "شماره تلفن ویلا نباید بیش از 11 حرف باشد")]
        [MinLength(11, ErrorMessage = "شماره تلفن ویلا نباید کمتر از 11 حرف باشد")]
        public string Mobile { get; set; }


        /// <summary>
        /// (قیمت کرایه یک روز ویلا (تومان 
        /// </summary>
        [Required(ErrorMessage = "قیمت کرایه روزانه  ویلا اجباری است ")]
        public long DayPrice { get; set; }


        /// <summary>
        /// قیمت فروش ویلا
        /// </summary>
        [Required(ErrorMessage = "قیمت فروش ویلا اجباری است ")]
        public long SellPrice { get; set; }

        /// <summary>
        /// (تاریخ ساخت ویلا  (روز و ماه باید 2 رقمی باشد 
        /// </summary>
        [Required]
        [DateValidation]
        public string BuildDate { get; set; }
    }
}
