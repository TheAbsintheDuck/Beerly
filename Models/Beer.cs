using System.ComponentModel.DataAnnotations;

namespace Backend_Task03.Models
{
    public class Beer
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
		[Required]
		public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public double? Percentage { get; set; }
        [Required]
        public string Brewery { get; set; }
        [Required]
        public string Country { get; set; }
		[RegularExpression("^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
		public string EAN13 { get; set; }
        public string? GoesWellWith { get; set; } = "-";
        public double? Rating { get; set; }
        public List<Review>? Reviews { get; set; }
		public List<Account>? FavoritedBy { get; set; }
		public string? PhotoPath { get; set; }
	}
}
