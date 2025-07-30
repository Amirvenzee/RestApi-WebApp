
using VILA.Api.Dtos;

namespace VILA.Api.Paging
{
    public class VilaAdminPaging :BasePaging
    {
        public List<VilaDto> VilaDtos { get; set; }
        public string Filter { get; set; }
    }
}
