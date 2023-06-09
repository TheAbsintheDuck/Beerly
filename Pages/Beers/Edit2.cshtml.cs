using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend_Task03.Data;
using Backend_Task03.Models;

namespace Backend_Task03.Pages.Beers
{
    public class Edit2Model : PageModel
    {
        private readonly AppDbContext database;

        public Edit2Model(AppDbContext context)
        {
            database = context;
        }

        [BindProperty]
        public Beer Beer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || database.Beers == null)
            {
                return NotFound();
            }

            var beer = await database.Beers.FirstOrDefaultAsync(m => m.ID == id);
            if (beer == null)
            {
                return NotFound();
            }
            Beer = beer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            database.Attach(Beer).State = EntityState.Modified;

            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(Beer.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BeerExists(int id)
        {
            return (database.Beers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var beer = await database.Beers.FindAsync(id);

			if (beer != null)
			{
				database.Beers.Remove(beer);
				await database.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
		public async Task<IActionResult> OnPostUpdateAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			database.Attach(Beer).State = EntityState.Modified;

			try
			{
				await database.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BeerExists(Beer.ID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}


	}
}