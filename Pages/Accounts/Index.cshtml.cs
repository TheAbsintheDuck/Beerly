using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Backend_Task03.Data;
using Backend_Task03.Models;

namespace Backend_Task03.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public IndexModel(AppDbContext context)
        {
            database = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (database.Accounts != null)
            {
                Account = await database.Accounts.ToListAsync();
            }
        }
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var account = await database.Accounts.FindAsync(id);

			if (account != null)
			{
				database.Accounts.Remove(account);
				await database.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
