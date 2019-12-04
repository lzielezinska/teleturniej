using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Data
{
    public static class IdentityDataInit
    {
        public static void SeedData(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            SeedRoles(_roleManager);
            SeedUsers(_userManager);
        }

        private static async void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });
            }

            if (!roleManager.RoleExistsAsync("Lecturer").Result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Lecturer" });
            }
        }

        private static async void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(userManager.FindByNameAsync("user").Result == null)
            {
                var user = new IdentityUser { UserName = "user@test.com", Email = "user@test.com" };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "Pass12#$");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser { UserName = "admin@test.com", Email = "admin@test.com" };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "Pass12#$");
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            if (userManager.FindByNameAsync("lecturer").Result == null)
            {
                var user = new IdentityUser { UserName = "lecturer@test.com", Email = "lecturer@test.com" };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "Pass12#$");
                    await userManager.AddToRoleAsync(user, "Lecturer");
                }
            }
        }
    }
}