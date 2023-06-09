using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Backend_Task03.Pages.Beers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
		private readonly AccessControl accessControl;

		public IndexModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			database = context;

		    accessControl = new AccessControl(database, httpContextAccessor);
		}

		[BindProperty(SupportsGet = true)]
		public string SelectedFoodType { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SelectedBeerType { get; set; }
		[BindProperty]
		public int BeerIdToAddToFavorites { get; set; }
		public IList<Beer> Beer { get; set; }

		[BindProperty(SupportsGet = true)]
		public string FindBeer { get; set; }

		[BindProperty(SupportsGet = true)]
		public string[] BeerType { get; set; }

		public string Chicken { get; set; }
		public string Meat { get; set; }
		public string Fish { get; set; }
		public string Vegetarian { get; set; }
		public string Dessert { get; set; }

		public async Task OnGetAsync(string searchInput)
		{
			IQueryable<Beer> beers = database.Beers;

			if (!string.IsNullOrEmpty(searchInput))
			{
				beers = beers.Where(b => b.Name.Contains(searchInput) || b.EAN13 == searchInput);
			}
			if (!string.IsNullOrEmpty(FindBeer))
			{
				beers = beers.Where(b => b.Name.Contains(FindBeer));
			}

			if (BeerType != null && BeerType.Length > 0)
			{
				List<string> types = new List<string>();
				if (BeerType.Contains("Ale"))
				{
					types.AddRange(new string[] { "Ale", "Brown Ale", "IPA", "Wheat Ale", "Belgian Ale", "Saison" });
				}
				if (BeerType.Contains("Lager"))
				{
					types.AddRange(new string[] { "Lager", "Kolsch" });
				}
				if (BeerType.Contains("Stout"))
				{
					types.AddRange(new string[] { "Stout", "Imperial Stout" });
				}
				beers = beers.Where(b => types.Contains(b.Type));
			}

			if (!string.IsNullOrEmpty(SelectedBeerType) && SelectedBeerType != "Beer")
			{
				beers = beers.Where(b => b.Type == SelectedBeerType);
			}
			if (!string.IsNullOrEmpty(SelectedFoodType) && SelectedFoodType != "Food")
			{
				beers = beers.Where(b => b.GoesWellWith.Contains(SelectedFoodType));
			}

			Beer = await beers.ToListAsync();

			var allBeers = await database.Beers.Include(b => b.Reviews).ThenInclude(r => r.FoodCategories).ToListAsync();

			foreach (var beer in allBeers)
			{
				if (beer.Reviews.Any())
				{
					decimal ratingValueCount = 0;
					decimal reviewCount = 0;

					Dictionary<string, int> categoryCounts = new Dictionary<string, int>();

					// Count the number of times each category appears in the reviews
					foreach (var review in beer.Reviews)
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
					beer.Rating = (double)totalRating;

					// Update the GoesWellWith property
					beer.GoesWellWith = string.Join(", ", mostSelectedCategories);
				}
			}

			await database.SaveChangesAsync();
		}

		public async Task OnPostAsync(string searchInput)
		{
			IQueryable<Beer> beers2Show = database.Beers;

			if (!string.IsNullOrEmpty(FindBeer))
			{
				beers2Show = beers2Show.Where(b => b.Name.Contains(FindBeer));
			}

			if (!string.IsNullOrEmpty(searchInput))
			{
				beers2Show = beers2Show.Where(b => b.Name.Contains(searchInput) || b.EAN13 == searchInput);
			}
			if (!string.IsNullOrEmpty(SelectedBeerType) && SelectedBeerType != "Beer")
			{
				beers2Show = beers2Show.Where(b => b.Type == SelectedBeerType);
			}
			if (!string.IsNullOrEmpty(SelectedFoodType) && SelectedFoodType != "Food")
			{
				beers2Show = beers2Show.Where(b => b.GoesWellWith.Contains(SelectedFoodType));
			}

			if (BeerType != null && BeerType.Length > 0)
			{
				List<string> types = new List<string>();
				if (BeerType.Contains("Ale"))
				{
					types.AddRange(new string[] { "Ale", "Brown Ale", "IPA", "Wheat Ale", "Belgian Ale", "Saison" });
				}
				if (BeerType.Contains("Lager"))
				{
					types.AddRange(new string[] { "Lager", "Kolsch", "STUFF HERE", "SORTER NÄR SOM NU" });
				}
				if (BeerType.Contains("Stout"))
				{
					types.AddRange(new string[] { "Stout", "Imperial Stout", "PUT THING HERE", "PUT IT HERE YES" });
				}
				beers2Show = beers2Show.Where(b => types.Contains(b.Type));
			}

            Beer = await beers2Show.ToListAsync();
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

            return RedirectToPage();
        }

		public bool IsFavorite(int beerId)
        {
            var account = accessControl.LoggedInAccount;
            return account != null && account.FavoriteBeers.Any(b => b.ID == beerId);
        }
    }
}