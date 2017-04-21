using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreMVC.Models
{
    public class Account
    {
        [Required]
        [Key]
        public int AccountID { get; set; }
        [StringLength(200)]
        [DisplayName("Địa Chỉ")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(10)]
        public string Password { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy H:mm}", ApplyFormatInEditMode = true)]
        public string CreatedDate { get; set; }

        public decimal Balance { get; set; }
        public bool Rememberme { get; set; }
        public bool IsHuman { get; set; }
    }
}
