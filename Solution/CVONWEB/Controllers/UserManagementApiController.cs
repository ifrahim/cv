using CVOnWeb.Models;
using CVOnWeb.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace CVOnWeb.Controllers
{
    public class UserManagementApiController : UmbracoApiController
    {
        [HttpPost]
        public FormResponse RegisterUser(RegisterModel registrationForm)
        {
            FormResponse response = new FormResponse();

            if (Helpers.Validators.IsValidEmail(registrationForm.RegisterUserEmailAddress))
            {
                var memberService = Services.MemberService;
                try
                {
                    if (memberService.GetByEmail(registrationForm.RegisterUserEmailAddress.ToLower()) != null)
                    {
                        response.ResponseCode = "error";
                        response.ResponseMessage = "This email address already exists, please login or try another email address";
                    }
                    else
                    {
                        var newUser = memberService.CreateMember(registrationForm.RegisterUserEmailAddress, registrationForm.RegisterUserEmailAddress, registrationForm.RegisterUserFullName, "candidate");
                        newUser.SetValue("fullName", registrationForm.RegisterUserFullName);
                        memberService.Save(newUser, false);
                        memberService.SavePassword(newUser, registrationForm.RegisterUserPassword);
                        memberService.AssignRole(newUser.Id, "CV Members");
                        memberService.Save(newUser, false);
                        
                        response.ResponseCode = "success";
                        response.ResponseData = Helpers.SiteHelper.GetRegistrationConfirmationPageUrl(Services.ContentService);
                        Helpers.Emailer.SendRegistrationEmail(newUser, Services.ContentService);
                    }
                }
                catch (Exception ex)
                {
                    response.ResponseCode = "Error";
                    response.ResponseMessage = "This email address already exists, please login or try another email address";
                }
            }
            else
            {
                response.ResponseCode = "Error";
                response.ResponseMessage = "Please enter a valid email address.";
            }
            return response;
        }

        //[HttpPost]
        //public void ForgotPassword(LoginModel form)
        //{
        //    var memberService = Services.MemberService;
        //    var thisUser = memberService.GetByEmail(form.UserEmailAddress.ToLower());
        //    if (thisUser != null)
        //    {
        //        var link = form.PasswordResetLink;
        //        var user = new User() { FullName = thisUser.Name, EmailAddress = form.UserEmailAddress, PasswordResetTimeStamp = DateTime.Now.AddMinutes(30) };
        //        link += string.Format("_token={0}", Utilities.Encryptor.Encrypt(JsonConvert.SerializeObject(user)));
        //        Helpers.Emailer.SendPasswordRequestEmail(user, link, Services.ContentService);
        //    }
        //    Redirect(form.ForgotPasswordLink);
        //}

        //[HttpPost]
        //public void LoginUser(LoginModel form)
        //{
        //    //var memberService = Services.MemberService;
        //    //var thisUser = memberService.GetByEmail(form.UserEmailAddress.ToLower());
        //    string url = "#";
        //    var contentService = Services.ContentService;
        //    if (1 == 1) // Logged In
        //    {
        //        url = Helpers.SiteHelper.GetNodeUrlByAlias(contentService, Constants.Umbraco.UmbracoPages.MY_ACCOUNT);
        //    }
        //    else
        //    {
        //        url = Helpers.SiteHelper.GetNodeUrlByAlias(contentService, Constants.Umbraco.UmbracoPages.LOGIN);
        //    }
        //    Redirect(url);
        //}
    }
}