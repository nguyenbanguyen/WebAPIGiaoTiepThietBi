using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VNPTThanhHoa_WebAPI.Models;

namespace VNPTThanhHoa_WebAPI.Data
{
    public class VNPTAPIContext : DbContext
    {
        public  VNPTAPIContext (DbContextOptions<VNPTAPIContext> options) : base(options)
        { }
        public DbSet<AccountApi>  Account { get; set; }
        public DbSet<ProductsApi> Products { get; set; }
    }
}
