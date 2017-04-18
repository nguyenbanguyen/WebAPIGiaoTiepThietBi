using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VNPTThanhHoa_WebAPI.Models
{
    /// <summary>
    /// Model quản lý product
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Identi number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Category của sản phẩm
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Giá tiền
        /// </summary>
        public decimal Price { get; set; }

    }
}
