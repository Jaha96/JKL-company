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
        public int CompanyId { get; set; }
        public  string CompanyName { get; set; }
        public  string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        //2-edit
        //3-delete

    }
}