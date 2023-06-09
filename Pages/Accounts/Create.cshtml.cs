using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Backend_Task03.Data;
using Backend_Task03.Models;

namespace Backend_Task03.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext database;

        public CreateModel(AppDbContext context)
        {
            database = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || database.Accounts == null || Account == null)
            {
                return Page();
            }

            database.Accounts.Add(Account);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
