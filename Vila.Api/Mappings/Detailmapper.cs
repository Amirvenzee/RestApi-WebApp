using VILA.Api.Dtos;
using VILA.Api.Models;

namespace VILA.Api.Mappings
{
    public static class Detailmapper
    {
        public static DetailDto ToDto(this Detail detail)
        {
            var dto = new DetailDto()
            {
                DetailId = detail.Id,
                Value = detail.Value,
                VilaId = detail.VilaId,
                What = detail.What,
                
            };
            return dto;
        }

        public static Detail ToDataModel(this DetailDto detail)
        {
            var dto = new Detail()
            {
                Id = detail.DetailId,
                Value = detail.Value,
                VilaId = detail.VilaId,
                What = detail.What,
            };
            return dto;
        }


    }
}
