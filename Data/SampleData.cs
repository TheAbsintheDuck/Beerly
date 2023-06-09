using Backend_Task03.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend_Task03.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Admin",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina",
                    Role = "User"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will",
                    Role = "User"
                });
            }

            database.SaveChanges();
        }

        public static void CreateBeer(AppDbContext database)
        {
            if (!database.Beers.Any())
            {
                var beers = new List<Beer>()
                {
                    new Beer
                    {
                        Name = "Hoppy McHopface",
                        Description = "A bold, hoppy brew with a crisp finish that will leave you feeling hoppily ever after.",
                        Type = "IPA",
                        Percentage = 6.5,
                        Brewery = "Hoppy Brewery",
                        Country = "USA",
                        EAN13 = "1940375440218",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Lager than Life",
                        Description = "A smooth, refreshing lager that's bigger than life itself.",
                        Type = "Lager",
                        Percentage = 4.8,
                        Brewery = "Big Brewery",
                        Country = "Canada",
                        EAN13 = "1154166643669",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Witty Kolsch",
                        Description = "A clever and light-bodied Kolsch that's sure to put a smile on your face.",
                        Type = "Kolsch",
                        Percentage = 5.0,
                        Brewery = "Wit Brewery",
                        Country = "Germany",
                        EAN13 = "0991733191373",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Saison's Greetings",
                        Description = "A spicy, seasonal Saison that's perfect for the holidays.",
                        Type = "Saison",
                        Percentage = 6.2,
                        Brewery = "Seasonal Brewery",
                        Country = "Belgium",
                        EAN13 = "3768642176626",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Hazy Daze IPA",
                        Description = "A hazy, juicy IPA that will transport you to a lazy day on the beach.",
                        Type = "IPA",
                        Percentage = 6.8,
                        Brewery = "Hazy Brewery",
                        Country = "USA",
                        EAN13 = "1222009428947",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Red Oktoberfest",
                        Description = "A rich, malty Oktoberfest brew with a deep red color and a smooth finish.",
                        Type = "Oktoberfest",
                        Percentage = 5.5,
                        Brewery = "Red Brewery",
                        Country = "Germany",
                        EAN13 = "4754493539030",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Imperial Stout of Mind",
                        Description = "A bold and complex Imperial Stout that will blow your mind.",
                        Type = "Imperial Stout",
                        Percentage = 10.0,
                        Brewery = "Mind Brewery",
                        Country = "USA",
                        EAN13 = "9614110758235",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Belgian Waffle Ale",
                        Description = "A sweet and savory Belgian-style ale that tastes like breakfast in a bottle.",
                        Type = "Belgian Ale",
                        Percentage = 7.0,
                        Brewery = "Waffle Brewery",
                        Country = "Belgium",
                        EAN13 = "8623813663884",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Irish Goodbye Stout",
                        Description = "A smooth and creamy Irish-style stout that will make you want to say goodbye to your troubles.",
                        Type = "Stout",
                        Percentage = 5.2,
                        Brewery = "Goodbye Brewery",
                        Country = "Ireland",
                        EAN13 = "8212798694862",
						PhotoPath = ""
					},
                    new Beer
                    {
                         Name = "Sourpuss Gose",
                        Description = "A tart and salty Gose that's perfect for those with a sourpuss attitude.",
                        Type = "Gose",
                        Percentage = 4.2,
                        Brewery = "Sourpuss Brewery",
                        Country = "USA",
                        EAN13 = "8991846926529",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Funky Monkey Brown Ale",
                        Description = "A nutty and chocolatey brown ale with a funky twist.",
                        Type = "Brown Ale",
                        Percentage = 5.8,
                        Brewery = "Funky Brewery",
                        Country = "USA",
                        EAN13 = "1442094668961",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Tropical Tripel",
                        Description = "A fruity and spicy Belgian Tripel with a tropical twist.",
                        Type = "Tripel",
                        Percentage = 9.5,
                        Brewery = "Tropical Brewery",
                        Country = "Belgium",
                        EAN13 = "0855394176653",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Java Stout",
                        Description = "A rich and roasty stout brewed with coffee beans for a java kick.",
                        Type = "Stout",
                        Percentage = 6.0,
                        Brewery = "Java Brewery",
                        Country = "USA",
                        EAN13 = "8648669673863",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Cucumber Kolsch",
                        Description = "A light and refreshing Kolsch brewed with cucumber for a cool twist.",
                        Type = "Kolsch",
                        Percentage = 4.5,
                        Brewery = "Cucumber Brewery",
                        Country = "USA",
                        EAN13 = "8431155282560",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Cherry Bomb Sour",
                        Description = "A tart and fruity sour brewed with cherries for a flavorful explosion.",
                        Type = "Sour",
                        Percentage = 5.0,
                        Brewery = "Cherry Brewery",
                        Country = "USA",
                        EAN13 = "4768974526181",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Honey Wheat Ale",
                        Description = "A light and smooth wheat ale brewed with honey for a touch of sweetness.",
                        Type = "Wheat Ale",
                        Percentage = 4.2,
                        Brewery = "Honey Brewery",
                        Country = "USA",
                        EAN13 = "8067715472180",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Gingerbread Stout",
                        Description = "A spiced and rich stout brewed with gingerbread for a holiday treat.",
                        Type = "Stout",
                        Percentage = 8.0,
                        Brewery = "Gingerbread Brewery",
                        Country = "USA",
                        EAN13 = "2909312251583",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Peanut Butter Porter",
                        Description = "A smooth and nutty porter brewed with peanut butter for a creamy finish.",
                        Type = "Porter",
                        Percentage = 6.5,
                        Brewery = "Peanut Brewery",
                        Country = "USA",
                        EAN13 = "1835571799362",
						PhotoPath = ""
					},
                    new Beer
                    {
                        Name = "Mango Habanero Wheat Ale",
                        Description = "A spicy and fruity wheat ale brewed with mango and habanero for a kick.",
                        Type = "Wheat Ale",
                        Percentage = 5.0,
                        Brewery = "Mango Habanero Brewery",
                        Country = "USA",
                        EAN13 = "1265144397501",
						PhotoPath = ""
					}
                };

                database.Beers.AddRange(beers);
                database.SaveChanges();
            }
        }

        public static void CreateFoodCategories(AppDbContext database)
        {
            if (!database.FoodCategories.Any())
            {
                var meatCategory = new FoodCategory { Name = "Meat" };
                var chickenCategory = new FoodCategory { Name = "Chicken" };
                var vegoCategory = new FoodCategory { Name = "Vegetarian" };
                var fishCategory = new FoodCategory { Name = "Fish" };
                var dessertCategory = new FoodCategory { Name = "Dessert" };

                database.FoodCategories.AddRange(meatCategory, chickenCategory, vegoCategory, fishCategory, dessertCategory);
                database.SaveChanges();
            }
        }

		public static void CreateReview(AppDbContext database)
		{
			if (!database.Reviews.Any())
			{
				var accounts = database.Accounts.ToList();
				var beers = database.Beers.ToList();
				var foodCategories = database.FoodCategories.ToList();
				var reviews = new List<Review>();
				var random = new Random();
				foreach (var beer in beers)
				{
					var reviewCount = random.Next(3, 6); // generate 3 to 5 reviews per beer
					for (int i = 0; i < reviewCount; i++)
					{
						var rating = random.Next(1, 6);
						var comment = GetRandomComment(beer.Name, rating);
						var account = accounts[random.Next(accounts.Count)];
						var created = GenerateRandomDateTime();
						var selectedCategories = new List<FoodCategory>();
						for (int j = 0; j < random.Next(1, 4); j++)
						{
							var category = foodCategories[random.Next(foodCategories.Count)];
							if (!selectedCategories.Contains(category))
							{
								selectedCategories.Add(category);
							}
						}
						var review = new Review
						{
							Rating = rating,
							Comment = comment,
							Created = created,
							Account = account,
							Beer = beer,
							FoodCategories = selectedCategories
						};
						reviews.Add(review);
					}
				}

				database.Reviews.AddRange(reviews);
				database.SaveChanges();
			}
		}

		// helper method to generate random comments based on beer name and rating
		private static string GetRandomComment(string beerName, int rating)
            {
                var comments = new List<string>
                {
                 "Nice fresh taste",
                 "Smooth and crisp",
                 "Bold and hoppy",
                 "Satisfying and refreshing",
                 "Rich and full-bodied",
                 "Delightfully hoppy",
                 "Aromatic and flavorful",
                 "Robust and flavorful",
                 "Refreshing and light",
                  "Fruity and aromatic"
                };
                var comment = comments[new Random().Next(comments.Count)];
                return $"{comment}.";
            }

		private static DateTime GenerateRandomDateTime()
		{
			Random random = new Random();
			int year = random.Next(2000, 2023);
			int month = random.Next(1, 13);
			int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
			int hour = random.Next(0, 24);
			int minute = random.Next(0, 60);
			return new DateTime(year, month, day, hour, minute, 0);
		}

		//Vad säger vi om det här? Har lagt den som static för att det ska funka - Vet ej om det är ok. / Linda
		public static List<string> CountryData { get; set; } = new List<string>
        {
        "Australia",
        "Belgium",
        "Canada",
        "Denmark",
        "Ecuador",
        "France",
        "Germany",
        "Honduras",
        "Ireland",
        "Jamaica",
        "Kenya",
        "Lebanon",
        "Mexico",
        "Netherlands",
        "Oman",
        "Portugal",
        "Russia",
        "Spain",
        "Turkey",
        "United States",
        "Vietnam",
        "Yemen",
        "Zimbabwe"
        };
    }
}