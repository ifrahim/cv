using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models
{
    public class Experience
    {
        public string ExperienceTitle { get; set; }
        public string ExperienceEmployer { get; set; }
        public string ExperienceSummary { get; set; }
        public DateTime ExperienceStartDate { get; set; }
        public DateTime ExperenceEndDate { get; set; }
        public int ExperienceOrder { get; set; }
    }
}