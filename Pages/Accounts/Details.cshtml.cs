using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Backend_Task03.Data;
using Backend_Task03.Models;
using System.Security.Principal;

namespace Backend_Task03.Pages.Accounts
{
	public class DetailsModel : PageModel
	{
		private readonly AppDbContext database;

		[BindProperty]
		public Beer Beer { get; set; } = default!;

		[BindProperty]
		public Account Account { get; set; } = default!;

		public List<Review> AccountReviews { get; set; } = new List<Review>();

		public DetailsModel(AppDbContext context)
		{
			database = context;
		}

		/*		public Account Account { get; set; } = default!;
		*/

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || database.Accounts == null)
			{
				return NotFound();
			}

			var account = await database.Accounts.Include(a => a.Reviews).ThenInclude(a => a.Beer).FirstOrDefaultAsync(m => m.ID == id);

			if (account == null)
			{
				return NotFound();
			}

			else
			{
				Account = account;
				AccountReviews = account.Reviews.ToList();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var review = await database.Reviews.FindAsync(id);

			if (review != null)
			{
				if (review != null)
				{
					database.Reviews.Remove(review);
					await database.SaveChangesAsync();
				}
			}
			string refererUrl = Request.Headers["Referer"].ToString();
			return Redirect(refererUrl);
		}
	}
}