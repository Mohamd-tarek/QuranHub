using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServerApp.Models
{
    public class IdentitySeedData
    {
        private const string username = "admin";
        private const string password = "Admin123$";
        private const string userRole = "user";

        public static async Task  SeedDatabase(IServiceProvider provider)
        {
            provider.GetRequiredService<IdentityDataContext>().Database.Migrate();

            UserManager<IdentityUser> userManager 
                = provider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager 
                = provider.GetRequiredService<RoleManager<IdentityRole>>();
           
           IdentityRole role = await roleManager.FindByNameAsync(userRole);
           IdentityUser user = await userManager.FindByNameAsync(username);

           if(role == null)
           {
               role = new IdentityRole(userRole);
               IdentityResult result = await roleManager.CreateAsync(role);
               if(!result.Succeeded)
               {
                   throw new Exception("Cannot create role : " + result.Errors.FirstOrDefault());
               } 
           }

           if(user == null)
           {
               user = new IdentityUser(username);
               IdentityResult result = await userManager.CreateAsync(user, password);
               if(!result.Succeeded)
               {
                   throw new Exception("Cannot create user : " + result.Errors.FirstOrDefault());
               } 
           }

           if(! await userManager.IsInRoleAsync(user, userRole)){
               IdentityResult result = await userManager.AddToRoleAsync(user, userRole);
               if(!result.Succeeded)
               {
                   throw new Exception("Cannot add user to role : " + result.Errors.FirstOrDefault());
               } 

            }
        }      
    }
}
