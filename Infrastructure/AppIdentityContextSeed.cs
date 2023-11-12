using Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "AbdElRahman",
                    Email = "AbdElRahman@gmail.com",
                    UserName = "AbdElRahman23",
                    Address = new Address
                    {
                        FirstName = "AbdElRahman",
                        LastName = "Saleh",
                        Street = "El-Zahraa st",
                        State = "Cairo",
                        City = "Wadi Hoff",
                        ZipCode = "11733"
                    }
                };

                await userManager.CreateAsync(user,"Password123?");
            }
        }
    }
}
