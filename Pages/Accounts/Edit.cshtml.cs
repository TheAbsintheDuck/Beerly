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

namespace Backend_Task03.Pages.Accounts
{
	public class EditModel : PageModel
	{
		private readonly AppDbContext database;

		public EditModel(AppDbContext context)
		{
			database = context;
		}

		[BindProperty]
		public Account Account { get; set; } = default!;

		private void LoadAccount(int id)
		{
			Account = database.Accounts.Include(a => a.Reviews).FirstOrDefault(a => a.ID == id);
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			
			LoadAccount(id);
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int id)
		{
			LoadAccount(id);
			bool success = await TryUpdateModelAsync(
				Account, nameof(Account),
				a => a.OpenIDIssuer,
				a => a.OpenIDSubject,
				a => a.Name,
				a => a.Role);
			if (success)
			{
				database.SaveChanges();
				return RedirectToPage("./Details");
			}

			else
			{
				return Page();
			}
		}

		private bool AccountExists(int id)
		{
			return (database.Accounts?.Any(e => e.ID == id)).GetValueOrDefault();
		}
	}
}
