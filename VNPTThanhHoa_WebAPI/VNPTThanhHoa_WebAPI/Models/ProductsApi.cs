using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;


namespace VNPTThanhHoa_WebAPI.Models
{

    /// <summary>
    /// Mock Product
    /// </summary>
    public class ProductsApi
    {
        /// <summary>
        /// identity number
        /// </summary>
        
        private int ProductsApiId { get; set; } 
        
        /// <summary>
        /// name of product
        /// </summary>
        /// 
        //[Required]
        public string Name { get; set; }
        /// <summary>
        /// category of product
        /// </summary>
        /// 
        //[Required]
        public string Category { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        /// 
        //[Required]
        public int Price { get; set; }

    }
}
