using Backend_Task03.Models;
using System.ComponentModel.DataAnnotations;

public class Review : IValidatableObject
{
	public int ID { get; set; }

	public int Rating { get; set; }

	[StringLength(180, MinimumLength = 0, ErrorMessage = "Comment must be at most 180 characters long.")]
	public string? Comment { get; set; }
	public DateTime Created { get; set; } = DateTime.Now;
	public Account Account { get; set; }
	public Beer Beer { get; set; }
	public List<FoodCategory> FoodCategories { get; set; } = new List<FoodCategory>();

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (string.IsNullOrWhiteSpace(Comment) && Rating == 0)
		{
			yield return new ValidationResult("Please enter a rating or a comment.", new[] { nameof(Rating), nameof(Comment) });
		}
	}

}