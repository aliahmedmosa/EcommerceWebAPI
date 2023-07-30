using APP.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Ali",
                    Email = "ali@ali.com",
                    UserName = "ali@ali.com",
                    Address = new Address
                    {
                        FirstName = "Ali",
                        LastName = "Mosa",
                        City = "Cairo",
                        State = "Nasr City",
                        Street = "Salah Salem",
                        ZipCode = "123"
                    }
                };
                var result = await userManager.CreateAsync(user,"P@$$w0rd");
            }
        }
    }
}
