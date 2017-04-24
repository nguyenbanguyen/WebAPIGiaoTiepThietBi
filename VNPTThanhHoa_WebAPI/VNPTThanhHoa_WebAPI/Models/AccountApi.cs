using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VNPTThanhHoa_WebAPI.Models
{
    public class AccountApi
    {
        [Required]
        public int Id { get; set; }

        public String Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Vui lòng nhập chính xác số điện thoại")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Số điện thoại phải từ 9 đến 11 số")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Vui lòng nhập chính xác số điện thoại")]

        public string PhoneNumber { get; set; }
    }
}
