using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models
{
    public class User
    {
        public string EmailAddress { get; set; } 
        public DateTime PasswordResetTimeStamp { get; set; }
        public string FullName { get; set; }

    }
}