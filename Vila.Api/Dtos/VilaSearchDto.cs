using System.ComponentModel.DataAnnotations;
using VILA.Api.ModelValidation;

namespace VILA.Api.Dtos
{
    public class VilaSearchDto
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
        public byte[] Image { get; set; }
        public List<DetailDto> Details { get; set; }
    }
}
