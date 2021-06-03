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
using System.Web.Security;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace CVOnWeb.Controllers
{
    public class UserManagementController : SurfaceController
    {
        public ActionResult ResetPasswordForm()
        {
            var model = new ResetPasswordModel();

            if(Request.QueryString["_token"] != null)
            {
                try
                {
                    var obj = Utilities.Encryptor.Decrypt(Request.QueryString["_token"]);
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(obj);
                    if(DateTime.Now < user.PasswordResetTimeStamp)
                    {
                        model.RequestIsValid = true;
                    }
                } catch (Exception ex)
                {
                    model.RequestIsValid = false;
                }
            }

            return PartialView("_ResetPasswordForm", model);
        }
        //[HttpPost]
        //public FormResponse RegisterUser(RegisterModel registrationForm)
        //{
        //    FormResponse response = new FormResponse();

        //    if (Helpers.Validators.IsValidEmail(registrationForm.RegisterUserEmailAddress))
        //    {
        //        var memberService = Services.MemberService;
        //        try
        //        {
        //            if (memberService.GetByEmail(registrationForm.RegisterUserEmailAddress.ToLower()) != null)
        //            {
        //                response.ResponseCode = "error";
        //                response.ResponseMessage = "This email address already exists, please login or try another email address";
        //            }
        //            else
        //            {
        //                var newUser = memberService.CreateMember(registrationForm.RegisterUserEmailAddress, registrationForm.RegisterUserEmailAddress, registrationForm.RegisterUserFullName, "candidate");
        //                memberService.Save(newUser, false);
        //                response.ResponseCode = "success";
        //                response.ResponseData = Helpers.SiteHelper.GetRegistrationConfirmationPageUrl(Services.ContentService);
        //                Helpers.Emailer.SendRegistrationEmail(newUser, Services.ContentService);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            response.ResponseCode = "Error";
        //            response.ResponseMessage = "This email address already exists, please login or try another email address";
        //        }
        //    } else
        //    {
        //        response.ResponseCode = "Error";
        //        response.ResponseMessage = "Please enter a valid email address.";
        //    }
        //    return response;
        //}

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel form)
        {
            var confirmationPage = (CurrentPage.GetProperty("confirmationPage").HasValue() ? ((IPublishedContent)CurrentPage.GetProperty("confirmationPage").GetValue()).Url  : "/forgot-password-confirmation/");
            var passwordResetPage = (CurrentPage.GetProperty("passwordResetPage").HasValue() ? ((IPublishedContent)CurrentPage.GetProperty("passwordResetPage").GetValue()).Url : "/password-reset/");
            
            var memberService = Services.MemberService;
            var thisUser = memberService.GetByEmail(form.FPUserEmailAddress.ToLower());
            if (thisUser != null)
            {
                var link = Request.Url.GetLeftPart(UriPartial.Authority); 
                link += passwordResetPage;
                var user = new User() { FullName = thisUser.Name, EmailAddress = form.FPUserEmailAddress, PasswordResetTimeStamp = DateTime.Now.AddMinutes(30) };
                link += string.Format("?_token={0}", Utilities.Encryptor.Encrypt(JsonConvert.SerializeObject(user)));
                Helpers.Emailer.SendPasswordRequestEmail(user, link, Services.ContentService);
            }

            var confirmationPageNode = (CurrentPage.GetProperty("confirmationPage").HasValue() ? ((IPublishedContent)CurrentPage.GetProperty("confirmationPage").GetValue()).Id : 1149);

            return RedirectToUmbracoPage(confirmationPageNode);
        }

        [HttpPost]
        public ActionResult LoginUser(LoginModel form)
        {
            var memberService = Services.MemberService;
            string url = "#";
            var contentService = Services.ContentService;

            if (Membership.ValidateUser(form.LoginUserEmailAddress, form.LoginUserPassword))
            {
                FormsAuthentication.SetAuthCookie(form.LoginUserEmailAddress, false); //, model.RememberMe);
                url = Helpers.SiteHelper.GetNodeUrlByAlias(contentService, Constants.Umbraco.UmbracoPages.MY_ACCOUNT);
            }
            else
            {
                url = Helpers.SiteHelper.GetNodeUrlByAlias(contentService, Constants.Umbraco.UmbracoPages.LOGIN);
                if(TempData.ContainsKey("ErrorMessage"))
                {
                    TempData["ErrorMessage"] = "Invalid username or password. Please try again.";
                }
                else
                {
                    TempData.Add("ErrorMessage", "Invalid username or password. Please try again.");
                }
            }

            return Redirect(url);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult SetNewPassword(ResetPasswordModel form)
        {
            var obj = Utilities.Encryptor.Decrypt(Request.QueryString["_token"]);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(obj);

            var memberService = Services.MemberService;

            var member = memberService.GetByUsername(user.EmailAddress);
            if(member != null)
            {
                memberService.SavePassword(member, form.FPUserPassword);
            }

            if(CurrentPage.GetProperty("resetPasswordConfirmationPage").HasValue())
            {
                return RedirectToUmbracoPage((IPublishedContent)CurrentPage.GetProperty("resetPasswordConfirmationPage").GetValue());
            } else
            {
                return Redirect("/");
            }
        }
    }
}