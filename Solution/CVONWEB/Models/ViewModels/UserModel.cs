using CVOnWeb.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models
{
    public class RegisterModel
    {
        public bool IsTrue { get { return true; } }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "*")]
        public string RegisterUserFullName { get; set; }

        [EmailAddress (ErrorMessage ="*")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string RegisterUserEmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        public string RegisterUserPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "*")]
        [Compare("RegisterUserPassword")]
        public string RegisterUserConfirmPassword { get; set; }

        //[Display(Name = "<span class=\"checkmark\"></span>I agree with terms and conditions")]

        [Display(Name = "I agree with terms and conditions")]
        [MustBeTrue(ErrorMessage = "You must accept the terms and conditions")]
        [Required(ErrorMessage = "*")]
        public bool AgreeTerms { get; set; }


    }
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string LoginUserEmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        public string LoginUserPassword { get; set; }

        public string ForgotPasswordLink { get; set; }
        public string PasswordResetLink { get; set; }
    }
    public class ForgotPasswordModel
    {
        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string FPUserEmailAddress { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "*")]
        public string FPUserPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "*")]
        [Compare("FPUserPassword")]
        public string FPUserConfirmPassword { get; set; }

        public string ForgotPasswordLink { get; set; }
        public string PasswordResetLink { get; set; }
    }

    public class ResetPasswordModel
    {
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "*")]
        public string FPUserPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "*")]
        [Compare("FPUserPassword")]
        public string FPUserConfirmPassword { get; set; }

        public bool RequestIsValid { get; set; }
    }
}