using VILA.Api.Dtos;

namespace VILA.Api.Paging
{
    public class VilaPaging:BasePaging
    {
        public List<VilaSearchDto> Vilas { get; set; }
        public string Filter { get; set; }
    }
}
