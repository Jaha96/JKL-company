using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JKLSite.Models
{
    public class CompanyContext:DbContext
    {
        public DbSet<Company> company { get; set; }
    }
}