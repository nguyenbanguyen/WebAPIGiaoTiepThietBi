using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace NetCoreMVC.Models
{
    /// <summary>
    /// tạo sẵn các giá trị khởi đầu vào DB
    /// </summary>
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NetCoreMVCContext(
                serviceProvider.GetRequiredService<DbContextOptions<NetCoreMVCContext>>()))
            {
                // Look for any account
                if (context.Account.Any())
                {
                    return; // Đã có DB thì return  
                }
                context.Account.AddRange(
                    //AccountID is [key] so can't be add value
                    new Account
                    {
                        //AccountID = 00001,
                        Address = "Thanh Hóa 1",
                        Password = "12345678",
                        Rememberme = true,
                        CreatedDate = DateTime.Now.ToString(),
                        Balance =100000
                    },
                    new Account
                    {
                        //AccountID = 00002,
                        Address = "Thanh Hóa 2",
                        Password = "12345678",
                        Rememberme = true,
                        CreatedDate = DateTime.Now.AddDays(1).ToString(),
                        Balance = 100000
                    },
                    new Account
                    {
                        //AccountID = 00003,
                        Address = "Thanh Hóa 3",
                        Password = "12345678",
                        Rememberme = true,
                        CreatedDate = DateTime.Now.AddDays(2).ToString(),
                        Balance = 100000
                    },
                    new Account
                    {
                        //AccountID = 00004,
                        Address = "Thanh Hóa 4",
                        Password = "12345678",
                        Rememberme = true,
                        CreatedDate = DateTime.Now.AddDays(3).ToString(),
                        Balance = 100000
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
