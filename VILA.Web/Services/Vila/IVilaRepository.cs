using VILA.Web.Models.Vila;
using VILA.Web.Services.Generic;

namespace VILA.Web.Services.Vila
{
    public interface IVilaRepository:IRepository<VilaModel>
    {
       Task<VilaPaging> Search(int pageId, string filter, int take,string token);
    }
}
