using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VNPTThanhHoa_WebAPI.Models
{
    /// <summary>
    /// Mock Product
    /// </summary>
    public class Products
    {
        /// <summary>
        /// identity number
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// name of product
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// category of product
        /// </summary>
        /// 
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        /// 
        [Required]
        public int Price { get; set; }

    }
}
