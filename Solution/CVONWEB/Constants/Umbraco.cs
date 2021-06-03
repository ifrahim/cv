using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Constants
{
    public static class Umbraco
    {
        public static class EmailTemplates
        {
            public static readonly string REGISTRATION_CONFIRMATION = "Registration Confirmation";
            public static readonly string PASSWORD_RESET_EMAIL = "Password Reset Email";
            public static readonly string PASSWORD_RESET_CONFIRMATION_EMAIL = "Password Reset Confirmation Email";
        }
        public static class UmbracoPages
        {
            public static readonly string MY_ACCOUNT = "myAccount";
            public static readonly string LOGIN  = "login";
        }
    }
}