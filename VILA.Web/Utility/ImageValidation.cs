
using System.Drawing;

namespace VILA.Web.Utility
{
    public static class ImageValidation
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var image = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
