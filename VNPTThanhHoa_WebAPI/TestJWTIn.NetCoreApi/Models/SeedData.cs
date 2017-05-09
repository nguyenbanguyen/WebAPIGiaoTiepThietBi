using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TestJWTIn.NetCoreApi.Models
{
    public class SeedData
    {
        public static IEnumerable<Contact> get()
        {
            return new List<Contact>
            {
                new Contact {  FirstName = "Joseph", LastName = "Grimes", Company = "GG Mechanical", JobTitle = "Vice President", Email = "jgrimes@ggmechanical.com", Phone = "414-367-4348", Street = "2030 Judah St", City = "San Francisco", PostalCode = "94144", State = "CA", PhotoUrl = "https://acquaint.blob.core.windows.net/images/josephgrimes.jpg" },
                new Contact {  FirstName = "Monica", LastName = "Green", Company = "Calcom Logistics", JobTitle = "Director", Email = "mgreen@calcomlogistics.com", Phone = "925-353-8029", Street = "230 3rd Ave", City = "San Francisco", PostalCode = "94118", State = "CA", PhotoUrl = "https://acquaint.blob.core.windows.net/images/monicagreen.jpg" },
                new Contact {  FirstName = "Joan", LastName = "Mancum", Company = "Bay Unified School District", JobTitle = "Principal", Email = "joan.mancum@busd.org", Phone = "914-870-7670", Street = "448 Grand Ave", City = "South San Francisco", PostalCode = "94080", State = "CA", PhotoUrl = "https://acquaint.blob.core.windows.net/images/joanmancum.jpg" }

            };
        }
        private const string adminUser = "Admin";
        private const string adminPassword = "A@a12345";
        //lỗi Dependency Injection with multi Constructors
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> UserManager = app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser user = await UserManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                //user = new ApplicationUser();
                user.UserName = adminUser;
                IdentityResult v= await UserManager.CreateAsync(user, adminPassword);
                if (v.Succeeded)
                {
                    int count = 0;
                    count++;
                }
                else
                {
                    int count = 0;
                    count++;
                }

            }
        }
        public static  void Seed(IApplicationBuilder app)
        {
            var Context = app.ApplicationServices.GetRequiredService<PeopleDbContext>();
            if (!Context.Contact.Any())
            {
                Context.Contact.AddRange(get());
            }
            Context.SaveChanges();
            //UserManager<ApplicationUser> UserManager = app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
            //ApplicationUser user = await UserManager.FindByIdAsync(adminUser);
            //if (user == null)
            //{
            //    user = new ApplicationUser();
            //    user.Id = adminUser;
            //    await UserManager.CreateAsync(user, adminPassword);
            //}
        }
    }
}
