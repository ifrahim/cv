using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models
{
    public class FormResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseData { get; set; }
    }
}