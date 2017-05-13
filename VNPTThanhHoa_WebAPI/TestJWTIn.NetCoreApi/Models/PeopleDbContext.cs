using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestJWTIn.NetCoreApi.Models
{
    public class PeopleDbContext : IdentityDbContext<ApplicationUser>
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contact{get;set;}
    }
}
