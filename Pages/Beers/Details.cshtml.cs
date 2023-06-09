using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Backend_Task03.Pages.Beers
{
	public class DetailsModel : PageModel
	{
		private readonly AppDbContext database;
		private readonly AccessControl accessControl;

		public DetailsModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			database = context;
			accessControl = new AccessControl(database, httpContextAccessor);
		}

		public Beer Beer { get; set; } = default!;
		public Review NewReview { get; set; }
		public Account Account { get; set; }

		[BindProperty]
		public List<string> ThisReviewFoodCategories { get; set; } = new List<string>();

		public void LoadBeer(int id)
		{
			Beer = database.Beers
				.Include(b => b.Reviews).ThenInclude(b => b.Account)
				.FirstOrDefault(b => b.ID == id);

			if (Beer == null)
			{
				return;
			}

			if (Beer.Reviews == null)
			{
				Beer.Reviews = new List<Review>();
			}

			NewReview = new Review
			{
				Beer = Beer,
				//oklart om denna ska ligga här
				FoodCategories = new List<FoodCategory>()
			};
		}

		public void ActiveAccount()
		{
			Account = accessControl.LoggedInAccount;
		}

		public IActionResult OnGet(int id)
		{
			Beer = database.Beers
			   .Include(b => b.Reviews).ThenInclude(r => r.FoodCategories)
			   .FirstOrDefault(b => b.ID == id);

			if (Beer.Reviews.Any())
			{
				decimal ratingValueCount = 0;
				decimal reviewCount = 0;

				Dictionary<string, int> categoryCounts = new Dictionary<string, int>();

				// Count the number of times each category appears in the reviews
				foreach (var review in Beer.Reviews)
				{
					foreach (var category in review.FoodCategories)
					{
						if (categoryCounts.ContainsKey(category.Name))
						{
							categoryCounts[category.Name]++;
						}
						else
						{
							categoryCounts[category.Name] = 1;
						}
					}

					if (review.Rating != 0)
					{
						ratingValueCount += review.Rating;
						reviewCount++;
					}
				}

				// Find the category/categories with the highest count
				List<string> mostSelectedCategories = new List<string>();
				int highestCount = 0;
				foreach (var kvp in categoryCounts)
				{
					if (kvp.Value > highestCount)
					{
						mostSelectedCategories.Clear();
						mostSelectedCategories.Add(kvp.Key);
						highestCount = kvp.Value;
					}
					else if (kvp.Value == highestCount)
					{
						mostSelectedCategories.Add(kvp.Key);
					}
				}

				// Update the Rating property
				decimal totalRating = Math.Round(ratingValueCount / reviewCount, 1);
				Beer.Rating = (double)totalRating;

				// Update the GoesWellWith property
				Beer.GoesWellWith = string.Join(", ", mostSelectedCategories);

				database.SaveChanges();
			}

			LoadBeer(id);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id, string? comment, int rating, List<string> food)
		{
			LoadBeer(id);
			ActiveAccount();

			NewReview.Comment = comment;
			NewReview.Rating = rating;
			NewReview.Account = Account;

			foreach (var selectedFood in new[] { "Chicken", "Meat", "Fish", "Vegetarian", "Dessert" })
			{
				if (food.Contains(selectedFood))
				{
					var foodCategory = database.FoodCategories.FirstOrDefault(c => c.Name == selectedFood);
					NewReview.FoodCategories.Add(foodCategory);
					ThisReviewFoodCategories.Add(selectedFood);
				}
			}

			bool success = await TryUpdateModelAsync(
					NewReview,
					nameof(NewReview),
					c => c.Rating,
					c => c.Comment
				);

			if (success)
			{
				Beer.Reviews.Add(NewReview);
				database.Reviews.Add(NewReview);
				database.SaveChanges();
				return RedirectToPage("./Details", new { id = Beer.ID, name = Beer.Name });
			}

			else
			{
				return Page();
			}
		}
		public async Task<IActionResult> OnPostToggleFavoriteAsync(int beerId)
		{
			var beer = database.Beers.Include(b => b.FavoritedBy).FirstOrDefault(b => b.ID == beerId);
			var account = accessControl.LoggedInAccount;

			if (beer != null && account != null)
			{
				if (account.FavoriteBeers.Any(b => b.ID == beerId))
				{
					account.FavoriteBeers.Remove(beer);
					beer.FavoritedBy.Remove(account);
				}
				else
				{
					account.FavoriteBeers.Add(beer);
					beer.FavoritedBy.Add(account);
				}

				await database.SaveChangesAsync();
			}

			return RedirectToPage(new { id = beerId, name = beer.Name });

		}
		public bool IsFavorite(int beerId)
		{
			var account = accessControl.LoggedInAccount;
			return account != null && account.FavoriteBeers.Any(b => b.ID == beerId);
		}

	}
}
