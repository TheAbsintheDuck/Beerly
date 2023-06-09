using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Task03.Pages.Shared.Components.SearchComponent
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly AppDbContext _database;

        public SearchViewComponent(AppDbContext database)
        {
            _database = database;
        }

        public async Task<IViewComponentResult> InvokeAsync(string findBeer, string[] beerType, string ean13)
        {
            IQueryable<Beer> beers2Show = _database.Beers;

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
                    
                }

                List<Beer> beers = await beers2Show.ToListAsync();

                return View(beers);
            }

            return View(new List<Beer>());
        }
    }
}
