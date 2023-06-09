using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Task03.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;
        private readonly FileRepository uploads;


        public IList<Beer> Beer { get; set; }

        public IndexModel(AppDbContext database, AccessControl accessControl, FileRepository uploads)
        {
            this.database = database;
            this.accessControl = accessControl;
            this.uploads = uploads;
        }


        public void OnGet()
        {
            Beer = database.Beers.ToList(); 

            if (Beer.Count > 0)
            {
                var random = new Random();
                var randomBeer = Beer[random.Next(Beer.Count)];
                string imageName = $"{randomBeer.Name}.png";
                string imagePath = Path.Combine("imagesBeerly", imageName);

                
                Beer = new List<Beer> { randomBeer }; 
                
            }
        }

        public async Task<IActionResult> OnPostAsync(string findBeer, string[] beerType, string ean13)
        {
            IQueryable<Beer> beers2Show = database.Beers;

            if (!string.IsNullOrEmpty(findBeer))
            {
                if (!string.IsNullOrEmpty(ean13))
                {
                    beers2Show = beers2Show.Where(b => b.EAN13 == ean13);
                }
                else if (!string.IsNullOrEmpty(findBeer))
                {
                    beers2Show = beers2Show.Where(b => b.Name.Contains(findBeer));
                }

                if (beerType != null && beerType.Any())
                {
                    List<string> types = new List<string>();
                    if (beerType.Contains("Ale"))
                    {
                        types.AddRange(new string[] { "Ale", "Brown Ale", "IPA", "Wheat Ale", "Belgian Ale", "Saison" });
                    }
                    if (beerType.Contains("Lager"))
                    {
                        types.AddRange(new string[] { "Lager", "Kolsch", "STUFF HERE", "SORTER NÄR SOM NU" });
                    }
                    if (beerType.Contains("Stout"))
                    {
                        types.AddRange(new string[] { "Stout", "Imperial Stout", "PUT THING HERE", "PUT IT HERE YES" });
                    }
                    beers2Show = beers2Show.Where(b => types.Contains(b.Type));
                }

                Beer = await beers2Show.ToListAsync();

                if (Beer.Count > 0)
                {
                    return RedirectToPage("./Beers", new { id = Beer[0].ID });
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile photo)
        {
            string path = Path.Combine(
                accessControl.LoggedInAccountID.ToString(),
                Guid.NewGuid().ToString() + "-" + photo.FileName
            );
            await uploads.SaveFileAsync(photo, path);
            return RedirectToPage();
        }

    }
}
