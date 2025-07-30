
using VILA.Api.Dtos;
using VILA.Api.Models;

namespace VILA.Api.Services.Detail
{
    public interface IDetailService
    {
        List<DetailDto> GetAllVilaDetails(int vilaId);
        Models.Detail getById(int Id);
        bool Create(Models.Detail model);
        bool Update(Models.Detail model);
        bool Delete(Models.Detail model);
        bool Save();
    }



      
    
}
