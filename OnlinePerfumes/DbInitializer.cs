using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.DataAccess;

namespace OnlinePerfumes
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Apply migrations automatically
                await context.Database.MigrateAsync();

                // Create roles
                string[] roles = { "Admin", "User", "Company" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Create default Admin user
                var adminEmail = "admin@example.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new ApplicationUser { UserName = "admin", Email = adminEmail };
                    string password = "Admin@123"; 
                    var result = await userManager.CreateAsync(adminUser, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }

}
