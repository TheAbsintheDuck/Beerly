using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Backend_Task03.Pages.BeerSuggestions
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;
        public IList<Beer> Beers { get; set; }

        public IndexModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            database = context;
            accessControl = new AccessControl(database, httpContextAccessor);
        }

        public async Task OnGetAsync()
        {
            var account = accessControl.LoggedInAccount;
            if (account != null)
            {
                Beers = await SuggestBeersOnget(account.ID);
            }
        }
        public async Task<List<Beer>> SuggestBeersOnget(int accountId)
        {
            var account = await database.Accounts
                .Include(a => a.FavoriteBeers)
                .FirstOrDefaultAsync(a => a.ID == accountId);

            if (account == null) return new List<Beer>();

            var favoriteTypes = account.FavoriteBeers.Select(b => b.Type).Distinct();
            var suggestedBeers = new List<Beer>();

            foreach (var type in favoriteTypes)
            {
                var favoriteBeerIds = account.FavoriteBeers.Select(b => b.ID).ToList();

                var beersOfThisType = await database.Beers
                    .Where(b => b.Type == type && !favoriteBeerIds.Contains(b.ID))
                    .Take(2)
                    .ToListAsync();

                suggestedBeers.AddRange(beersOfThisType);
            }

            return suggestedBeers;
        }
    }
}