using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NetCoreMVC.Models
{
    public class Account
    {
        [Required]
        [Key]
        public int AccountID { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy H:mm}", ApplyFormatInEditMode = true)]
        public string CreatedDate { get; set; }

        public decimal Balance { get; set; }
        public bool Rememberme { get; set; }
    }
}
