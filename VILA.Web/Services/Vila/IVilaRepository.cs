using VILA.Web.Models.Vila;

namespace VILA.Web.Services.Vila
{
    public interface IVilaRepository
    {
       Task<VilaPaging> Search(int pageId, string filter, int take,string token);
    }
}
