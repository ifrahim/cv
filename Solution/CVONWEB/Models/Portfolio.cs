using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models
{
    public class Portfolio
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<string> PortfolioMedia { get; set; }
    }
}