using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using talabat.core.Entites.identity;

namespace talabat.Apis.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser?> FindUserWithEmailAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = userManager.Users.Include(U => U.Address).FirstOrDefault(U => U.Email == email);

            return user;

       
        }
    }
}
