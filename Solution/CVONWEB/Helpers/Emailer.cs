using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using CVOnWeb.Models;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace CVOnWeb.Helpers
{
    public static class Emailer
    {
        public static void SendRegistrationEmail(IMember user, IContentService contentService)
        {
            var emailTemplate = SiteHelper.GetEmailRepository(contentService, Constants.Umbraco.EmailTemplates.REGISTRATION_CONFIRMATION);

            if (emailTemplate != null)
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailEmail"]);
                MailAddress to = new MailAddress(user.Email, user.Name);
                string toname = user.Name;
                string subject = emailTemplate.GetProperty("subject").GetValue().ToString();
                string body = emailTemplate.GetProperty("body").GetValue().ToString().Replace("{name}", user.Name);

                SendEmail(from, to, subject, body);
            }
        }
        public static void SendPasswordRequestEmail(Models.User user, string link, IContentService contentService)
        {
            var emailTemplate = SiteHelper.GetEmailRepository(contentService, Constants.Umbraco.EmailTemplates.PASSWORD_RESET_EMAIL);

            if (emailTemplate != null)
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailEmail"]);
                MailAddress to = new MailAddress(user.EmailAddress, user.FullName);
                string subject = emailTemplate.GetProperty("subject").GetValue().ToString();
                string body = emailTemplate.GetProperty("body").GetValue().ToString().Replace("{link}", link).Replace("{name}", user.FullName);

                SendEmail(from, to, subject, body);
            }
        }


        //public static void SendPasswordResetConfirmationEmail(IMember user, IContentService contentService)
        //{
        //    var emailTemplate = SiteHelper.GetEmailRepository(contentService, Constants.Umbraco.EmailTemplates.PASSWORD_RESET_CONFIRMATION_EMAIL);

        //    if (emailTemplate != null)
        //    {
        //        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailEmail"]);
        //        MailAddress to = new MailAddress(user.Email, user.Name);
        //        string toname = user.Name;
        //        string subject = emailTemplate.GetProperty("subject").GetValue().ToString();
        //        string body = emailTemplate.GetProperty("body").GetValue().ToString().Replace("{name}", user.Name);

        //        SendEmail(from, to, subject, body);
        //    }
        //}
        //public static void SendPasswordChangeEmail(IMember user, IContentService contentService, string link)
        //{
        //    var emailTemplate = SiteHelper.GetEmailRepository(contentService, Constants.Umbraco.EmailTemplates.PASSWORD_RESET_EMAIL);

        //    if (emailTemplate != null)
        //    {
        //        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailEmail"]);
        //        MailAddress to = new MailAddress(user.Email, user.Name);
        //        string toname = user.Name;
        //        string subject = emailTemplate.GetProperty("subject").GetValue().ToString();
        //        string body = emailTemplate.GetProperty("body").GetValue().ToString().Replace("{link}", link).Replace("{name}", user.Name);

        //        SendEmail(from, to, subject, body);
        //    }
        //}
        public static void SendEmail(MailAddress from, MailAddress to, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.Body = body;
            msg.From = from;
            msg.IsBodyHtml = true;
            msg.Subject = subject;
            msg.To.Add(to);
            int port = 2525;
            int.TryParse(ConfigurationManager.AppSettings["mailPort"], out port);
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["mailServer"], port);
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["mailUser"], ConfigurationManager.AppSettings["mailPassword"]);
            smtpClient.Send(msg);
        }
    }
}