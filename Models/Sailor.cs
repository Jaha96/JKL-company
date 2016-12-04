using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JKLSite.Models
{
    public class Sailor
    {
        public int SailorId { get; set; }
        public string SailorName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int MaritialStatus { get; set; }
        public string Address { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; }
        public int ShoeSize { get; set; }
        public int JobStatus { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public int VacationMonth { get; set; }
    }
}