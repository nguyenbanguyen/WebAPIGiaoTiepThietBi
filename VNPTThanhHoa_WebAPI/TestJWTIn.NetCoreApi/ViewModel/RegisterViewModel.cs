using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestJWTIn.NetCoreApi.ViewModel
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        [StringLength(30,MinimumLength =6)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(12,MinimumLength =8)]
        public string Password { get; set; }
        public String UserName { get; set; }
    }
}
