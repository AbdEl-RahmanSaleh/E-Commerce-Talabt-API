using Core;
using Core.IdentityEntities;
using Core.IdentityEntities.IdentityContext;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                   
                    var identityConext = services.GetRequiredService<AppIdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await AppIdentityContextSeed.SeedUserAsync(userManager);

                    
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
