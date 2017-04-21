using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCoreMVC.Models
{
    public class NetCoreMVCContext : DbContext
    {
        public NetCoreMVCContext (DbContextOptions<NetCoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<NetCoreMVC.Models.Account> Account { get; set; }
    }
}
