using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("MeterReading")]
    public class MeterReadingDTO
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime MeterReadingDateTime { get; set; }
        [Required]
        [MaxLength(5)]
        public string? MeterReadingValue { get; set; }
    }
}
