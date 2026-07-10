using Microsoft.AspNetCore.Identity;

namespace SakilaApp.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Administrator", "Employee" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        string emailAdmin = "admin@espe.edu.ec";
        string passwordAdmin = "Admin123*";

        var admin = await userManager.FindByEmailAsync(emailAdmin);

        if (admin == null)
        {
            admin = new IdentityUser
            {
                UserName = emailAdmin,
                Email = emailAdmin,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, passwordAdmin);
        }

        if (!await userManager.IsInRoleAsync(admin, "Administrator"))
        {
            await userManager.AddToRoleAsync(admin, "Administrator");
        }

        string emailEmployee = "employee@espe.edu.ec";
        string passwordEmployee = "Employee123*";

        var employee = await userManager.FindByEmailAsync(emailEmployee);

        if (employee == null)
        {
            employee = new IdentityUser
            {
                UserName = emailEmployee,
                Email = emailEmployee,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(employee, passwordEmployee);
        }

        if (!await userManager.IsInRoleAsync(employee, "Employee"))
        {
            await userManager.AddToRoleAsync(employee, "Employee");
        }
    }
}
