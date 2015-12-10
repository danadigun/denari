using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        //Employee Details
        //[Required(ErrorMessage = "please enter employee's First name")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "please enter employee's Last name")]
        public string LastName { get; set; }

       //[Required(ErrorMessage = "please enter employee's home address")]
        public string Address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        //Security credentials
        public string role { get; set; }

        //[Required(ErrorMessage = "Username field cannot be empty")]
        public string UserName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

}