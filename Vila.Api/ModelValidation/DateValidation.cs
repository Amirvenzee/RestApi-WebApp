
using System.ComponentModel.DataAnnotations;
using VILA.Api.Dtos;
using VILA.Api.Utility;

namespace VILA.Api.ModelValidation
{
    public class DateValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vila = (VilaDto)validationContext.ObjectInstance;

            try
            {
                var date = vila.BuildDate.ToEnglishDateTime();

                if (date > DateTime.Now)
                    return new ValidationResult("تاریخ ساخت باید در زمان گذشته باشد");

                return ValidationResult.Success;
            }
            catch 
            {
                return new ValidationResult("(.تاریخ ساخت باید در فرمت 1404/01/12 باشد (ماه  و روز 2 رقمی باشد ");
                
            }
        }
    }
}
