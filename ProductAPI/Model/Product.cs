using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [DataType("nvarchar")]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(150)]
        public string? ProductDescription { get; set; } = string.Empty;

        public double? ProductPrice { get; set; } = 0;
        public int? ProductStock { get; set; } = 0;

        public DateTime? ProductDateTimeSave { get; set; } = DateTime.Now;
    }
}
