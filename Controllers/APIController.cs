using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Backend_Task03.Controllers
{
    [Route("/api")]
    [ApiController]
    [AllowAnonymous] // lägger till den här för säkerhets skull, enligt instruktion från Jakob
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }

      

        [HttpGet]
        public IActionResult Get([FromQuery] string category)
        {
            var beer = GetBeer(category);

            if (beer == null)
            {
                return NotFound(); // Return a 404 Not Found response if the beer is not found
            }

            // Get a random image URL
            var imageUrl = GetImageUrl(beer.Name);

            // Extract only the desired properties from the beer object
            var beerResponse = new
            {
                Name = beer.Name,
                Description = beer.Description,
                Type = beer.Type,
                Percentage = beer.Percentage,
                Brewery = beer.Brewery,
                Country = beer.Country,
                GoesWellWith = beer.GoesWellWith,
                ImageUrl = imageUrl
            };

            return Ok(beerResponse);

        }

        private Beer GetBeer(string category)
        {
            // Retrieve the beer from the database based on the "goesWellWith" value
            // Replace this with your actual database query implementation

            var beers = database.Beers.Where(b => b.GoesWellWith.Contains(category)).ToList();

            if (beers.Count == 0)
            {
                return null; // No beers found for the given category
            }

            var random = new Random();
            var randomIndex = random.Next(0, beers.Count);
            var randomBeer = beers[randomIndex];

            return randomBeer;
        }

        private string GetImageUrl(string name)
        {

            var imageFiles = Directory.GetFiles("wwwroot/imagesBeerly");

            if (imageFiles.Length == 0)
            {
                return null; // No images available
            }


            var thisImage = imageFiles.Where(c => c.Contains(name)).FirstOrDefault();


            var imageFileName = Path.GetFileName(thisImage);

            // Construct the image URL 

            //Correct for deployment
            var imageUrl = $"https://beerly.azurewebsites.net/imagesBeerly/{name}" + ".png";

            //Test: Works only in dev
            //var imageUrl = $"https://localhost:5000//imagesBeerly/{name}" + ".png";

            return imageUrl;
        }


    }

}
