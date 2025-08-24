using System.ComponentModel.DataAnnotations;

namespace VILA.Api.Models
{
    public class Vila
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string State { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required]
        public long DayPrice { get; set; }

        [Required]
        public long SellPrice { get; set; }

        [Required]
        public DateTime BuildDate { get; set; }

        public byte[]? Image { get; set; }

        public List<Detail> Details { get; set; }

    }
}
