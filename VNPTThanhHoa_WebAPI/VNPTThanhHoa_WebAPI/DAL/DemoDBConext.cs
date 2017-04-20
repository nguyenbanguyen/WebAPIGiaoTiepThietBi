using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VNPTThanhHoa_WebAPI.Models;

namespace VNPTThanhHoa_WebAPI.DAL
{
    public class DemoDBConext :DbContext
    {
        public DbSet<Products>  Products{ get; set; }
        public DbSet<Account> Account { get; set; }
    }
}
