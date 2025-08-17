using VILA.Web.Models.Vila;

namespace VILA.Web.Services.Vila
{
    public interface IVilaRepository
    {
        VilaPaging Search(int pageId, string filter, int take);
    }
}
