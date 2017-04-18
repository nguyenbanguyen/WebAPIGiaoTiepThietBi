using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VNPTThanhHoa_WebAPI.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VNPTThanhHoa_WebAPI.Controllers
{
    /// <summary>
    /// api class product
    /// </summary>
    [RequireHttps]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       
        [HttpGet]
        [Route("Get100Products")]
        public IEnumerable<Products> Get100Product()
        {
            Products[] LongProduct = new  Products[100];
            for (int i = 0; i < 100; i++)
            {
                LongProduct[i] = new Products { Id = i,Name="cà loại "+i, Category= "cà",Price =10000  };
            }
            return LongProduct;
        }
        /// <summary>
        /// get by amount
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns> Enumerable of products</returns>
        /// 
        [Route("GetProductByAmount/{Amount}")]
        [HttpGet]
        public IEnumerable<Products> GetProductByAmount(int Amount)
        {
            Products[] LongProduct = new Products[Amount];
            for (int i = 0; i < Amount; i++)
            {
                LongProduct[i] = new Products { Id = i, Name = "cà loại " + i, Category = "cà", Price = 10000 };
            }
            return LongProduct;
        }
    }

}
