using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JKLSite.Models
{
    public class ServiceModel
    {
        public int HistoryId { get; set; }
        public string SailorName { get; set; }
        public int SailorId { get; set; }
        public int RankId { get; set; }
        public int VesselId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeriodMonth { get; set; }

    }
}