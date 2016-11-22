using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKLSite.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public int CompanyId { get; }
        public  string CompanyName { get; set; }
        public  string ContactPerson { get; set; }
        public static string Email { get; set; }
        public static string Phone { get; set; }
        public static string Address { get; set; }
        public static string Password { get; set; }

    }
}