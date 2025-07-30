using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VILA.Api.Models
{
    public class Detail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VilaId { get; set; }

        [Required]
        [MaxLength(255)]
        public string What { get; set; }

        [Required]
        [MaxLength(500)]
        public string Value { get; set; }



        [ForeignKey("VilaId")]
        public Vila Vila { get; set; }
    }
}
