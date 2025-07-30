using VILA.Api.Dtos;
using VILA.Api.Models;
using VILA.Api.Paging;

namespace VILA.Api.Services.Vila
{
    public interface IVilaService
    {
        List<VilaDto> GetAll();
        Models.Vila GetById(int id);
        bool Create(Models.Vila vila);
        bool Update(Models.Vila vila);
        bool Delete(Models.Vila vila);
        VilaPaging Search(int pageId, string filter,int take);
        VilaAdminPaging SearchVilaAdmin(int pageId, string filter, int take);
        bool Save();
    }
}
