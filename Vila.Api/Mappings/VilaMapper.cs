using System.Runtime.CompilerServices;
using VILA.Api.Dtos;
using VILA.Api.Models;
using VILA.Api.Utility;
namespace VILA.Api.Mappings
{
    public static class VilaMapper
    {


        public static VilaDto ToDto(this Models.Vila vila)
        {
            return new VilaDto()
            {
                Id = vila.Id,
                Address = vila.Address,
                BuildDate = vila.BuildDate.ToPersainDate(),
                City = vila.City,
                Mobile = vila.Mobile,
                State = vila.State,
                Name = vila.Name,
                DayPrice = vila.DayPrice,
                SellPrice = vila.SellPrice,

            };
        }

        public static Models.Vila ToDataModel(this VilaDto vila)
        {
            return new Models.Vila()
            {
                Id = vila.Id,
                Address = vila.Address,
                BuildDate = vila.BuildDate.ToEnglishDateTime(),
                City = vila.City,
                Mobile = vila.Mobile,
                State = vila.State,
                Name = vila.Name,
                DayPrice = vila.DayPrice,
                SellPrice = vila.SellPrice,

            };
        }

        

        public static List<VilaSearchDto> ToSearchDto(this List<Models.Vila> vila)
        {
             var list = vila.Select(x => new VilaSearchDto()
            {
                DayPrice = x.DayPrice,
                SellPrice = x.SellPrice,
                City = x.City,
                Mobile = x.Mobile,
                State = x.State,
                Name = x.Name,
                Address = x.Address,
                Id = x.Id,
                BuildDate = x.BuildDate.ToPersainDate(),

                Details = x.Details.Select(s=>new DetailDto
                {
                    DetailId = s.Id,
                    Value = s.Value,
                    VilaId = s.VilaId,
                    What = s.What,
                }).ToList()
                

            });
            return list.ToList();

        }

        public static List<VilaDto> ToDto(this List<Models.Vila> vila)
        {
            var list = vila.Select(x => new VilaDto()
            {
                DayPrice = x.DayPrice,
                SellPrice = x.SellPrice,
                City = x.City,
                Mobile = x.Mobile,
                State = x.State,
                Name = x.Name,
                Address = x.Address,
                Id = x.Id,
                BuildDate = x.BuildDate.ToPersainDate(),
                



            });
            return list.ToList();

        }
    }
    
}
