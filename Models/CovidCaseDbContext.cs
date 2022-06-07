using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    /**
     * Class for doing all crud operations to database
     * @Author: Yang Yang
     */
    public class CovidCaseDbContext:DbContext
    {
        public DbSet<CovidCase> covidCases { get; set; }
    }
}