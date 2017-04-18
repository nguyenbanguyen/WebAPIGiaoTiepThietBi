using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VNPTThanhHoa_WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VNPTThanhHoa_WebAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        /// <summary>
        /// Controller demo quản lý hiển thị Products
        /// </summary>
        
            Products[] Products = new Products[]
            {
            new Products { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Products { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Products { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            };
            /// <summary>
            /// Lấy array  100 product với id , tên chạy loop 1-100
            /// </summary>
            /// <returns></returns>
            /// 
           
            [HttpGet]
            public IEnumerable<Products> Get100Product()
            {
                Products[] LongProduct = new Models.Products[100];
                for (int i = 0; i < 100; i++)
                {
                    LongProduct[i] = new Products { Id = i, Name = "Táo loại " + i, Category = "táo", Price = 10000 };
                }
                return LongProduct;
            }/// <summary>
             /// Trả về array Product dựa vào parameter NumberOfProduct bằng Json ,giới hạn max 9999 để tránh sử dụng tài nguyên quá nhiều
             /// </summary>
             /// <param name="NumberOfProduct"> số lượng sản phẩm</param>
             /// <returns></returns>
             /// 

            [Route("GetProductByAmount/{NumberOfProduct}")]
            [HttpGet]
            public IEnumerable<Products> GetProductByAmount(int NumberOfProduct)
            {
                if (NumberOfProduct <= 9999)
                {
                    Products[] LongProduct = new Models.Products[NumberOfProduct];
                    for (int i = 0; i < NumberOfProduct; i++)
                    {
                        LongProduct[i] = new Products { Id = i, Name = "Cà loại " + i, Category = "Cà", Price = 10000 };
                    }
                    return LongProduct;
                }
                else return Products;
            }



        }
    
}
