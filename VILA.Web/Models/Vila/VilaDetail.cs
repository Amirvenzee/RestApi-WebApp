using AspNetCoreGeneratedDocument;
using System.ComponentModel.DataAnnotations;
using VILA.Web.Models.Detail;

namespace VILA.Web.Models.Vila
{
    public class VilaDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string address { get; set; }
        public string Mobile { get; set; }
        public long DayPrice { get; set; }
        public long SellPrice { get; set; }
        public string BuildDate { get; set; }
        public byte[]? Image { get; set; }
        public List<DetailModel> Detail { get; set; }


        public static VilaDetail OneVilaDetail(VilaModel vila , List<DetailModel> details)
        {
        

            return new VilaDetail
            {
                Id = vila.Id,
                Name = vila.Name,
                State = vila.State,
                City = vila.City,
                address = vila.address,
                Mobile = vila.Mobile,
                DayPrice = vila.DayPrice,
                SellPrice = vila.SellPrice,
                BuildDate = vila.BuildDate,
                Image = vila.Image,
                Detail = details.Select(x=> new DetailModel
                {
                    What = x.What,
                    VilaId = x.VilaId,
                    DetailId = x.DetailId,
                    Value = x.Value,
                }).ToList()
             
            };
        }
    }
}
