
using VILA.Web.Models.Detail;
namespace VILA.Web.Models.Vila
{
    public class VilaSearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public long DayPrice { get; set; }
        public long SellPrice { get; set; }
        public string BuildDate { get; set; }
        public List<DetailModel> Details { get; set; }
    }
}
