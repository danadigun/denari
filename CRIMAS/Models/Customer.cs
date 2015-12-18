using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string AccountNo { get; set; } //auto-generated

        [Required(ErrorMessage="Name field cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Next of kin field cannot be empty")]
        public string NextOfkin { get; set; }

        [Required(ErrorMessage = "A cooperator must be employed. please state occupational address!")]
        public string OfficeAddress { get; set; }

        [Required(ErrorMessage = "Residential address field cannot be empty")]
        public string ResidentialAddress { get; set; }
       
        public string employer { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }

        [Required(ErrorMessage = "field cannot be empty")]
        public string StateOfOrigin { get; set; }
        public string DateCreated { get; set; }//auto-generated

    }
}