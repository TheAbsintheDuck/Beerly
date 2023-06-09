using Backend_Task03.Data;
using Backend_Task03.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace Backend_Task03.Data
{
    public class AccessControl
    {
        public int LoggedInAccountID { get; set; }
        public string LoggedInAccountName { get; set; }
        public string LoggedInAccountRole { get; set; }
        public Account LoggedInAccount { get; set; }

        public AccessControl(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext.User;
            string subject = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            string issuer = user.FindFirst(ClaimTypes.NameIdentifier).Issuer;

            LoggedInAccount = db.Accounts.Include(a => a.FavoriteBeers).Single(p => p.OpenIDIssuer == issuer && p.OpenIDSubject == subject);
            LoggedInAccountID = LoggedInAccount.ID;
            LoggedInAccountName = user.FindFirst(ClaimTypes.Name).Value;
            LoggedInAccountRole = LoggedInAccount.Role;
           

        
        }
    }
}
