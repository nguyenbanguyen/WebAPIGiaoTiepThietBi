using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNPTThanhHoa_WebAPI.Models;

namespace VNPTThanhHoa_WebAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VNPTAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<VNPTAPIContext>>()))
            {
                // Look for any account
                if (context.Products.Any())
                {
                    return; // Đã có DB thì return  
                }
                //context.Products.AddRange(
                //    //AccountID is [key] so can't be add value
                //    new ProductsApi
                //    {

                //        Name="Cá hồi",
                //        Category="Food",
                //        Price=100000
                //    },
                //    new ProductsApi
                //    {
                       
                //        Name = "Cá hồi 2",
                //        Category = "Food",
                //        Price = 100000
                //    },
                //    new ProductsApi
                //    {
                //        Name = "Cá hồi 3",
                //        Category = "Food",
                //        Price = 100000
                //    },
                //    new ProductsApi
                //    {
                //        Name = "Cá hồi 4",
                //        Category = "Food",
                //        Price = 100000
                //    }
                //    );
                //context.SaveChanges();
            }

        }
    }
}
