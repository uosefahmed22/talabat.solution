using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.identity;

namespace talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUSerAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Yousef Ahmed",
                    Email = "Uosef0022@gmail.com",
                    UserName = "Uossef",
                    PhoneNumber = "01032043047",
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
