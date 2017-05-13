using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VNPTThanhHoa_WebAPI.Models;
using VNPTThanhHoa_WebAPI.Data;

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
        private VNPTAPIContext VnptDB;
        public ProductController(VNPTAPIContext VnptDbContext)
        {
            VnptDB = VnptDbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       
        [HttpGet]
        [Route("GetAllProducts")]
        public IEnumerable<ProductsApi> GetAllProduct()
        {
            var AllProduct = VnptDB.Products.ToList();
            
            
            return AllProduct;
        }
        /// <summary>
        /// get by amount
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns> Enumerable of products</returns>
        /// 
        [Route("GetProductByAmount/{Amount}")]
        [HttpGet]
        public IEnumerable<ProductsApi> GetProductByAmount(int Amount)
        {
            ProductsApi[] LongProduct = new ProductsApi[Amount];
            for (int i = 0; i < Amount; i++)
            {
                LongProduct[i] = new ProductsApi {  Name = "cà loại " + i, Category = "cà", Price = 10000 };
            }
            return LongProduct;
        }
        [Route("AddNewProducts")]
        [HttpPost]
        public IActionResult AddNewProducts([FromBody][Bind("Name,Category,Price")] ProductsApi product)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    VnptDB.Products.Add(product);
                    VnptDB.SaveChanges();
                    //return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
                    return Ok();
                }
            }
            catch (Exception ex /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +ex.ToString()+
                    "see your system administrator.");
            }
            return BadRequest();
        }
        [Route("GetProductById/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = VnptDB.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [Route("DeleteProductById/{id}")]
        [HttpDelete]
        public IActionResult DeletebyId (int id)
        {
            var item = VnptDB.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            VnptDB.Products.Remove(item);
            VnptDB.SaveChanges();
            return new ObjectResult(item);
        }
        
    }

}
