using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Backend_Task03.Data;
using Backend_Task03.Models;
using Backend_Task03.Utilities;

namespace Backend_Task03.Pages.Beers
{
    public class CreateModel : PageModel
    {
        private readonly Backend_Task03.Data.AppDbContext database;

        public CreateModel(Backend_Task03.Data.AppDbContext context)
        {
            database = context;
        }

        [BindProperty]
        public Beer Beer { get; set; } = default!;

        public IActionResult OnGet(bool generateEAN13 = false)
        {
            Beer = new Beer { EAN13 = EAN13.GenerateEAN13() };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(IFormFile photo)
        {
            if (!ModelState.IsValid || database.Beers == null || Beer == null)
            {
                return Page();
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            var filePath = Path.Combine("wwwroot", "imagesBeerly", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            TempData["PhotoPath"] = "/imagesBeerly/" + fileName;

            if (TempData["PhotoPath"] != null)
            {
                Beer.PhotoPath = TempData["PhotoPath"].ToString();
            }

            Beer.EAN13 = EAN13.GenerateEAN13();
            database.Beers.Add(Beer);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
