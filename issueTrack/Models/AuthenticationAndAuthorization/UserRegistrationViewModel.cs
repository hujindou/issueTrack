using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace issueTrack.Models.AuthenticationAndAuthorization
{
    /// <summary>
    /// User registion model
    /// </summary>
    public class UserRegistrationViewModel
    {
        [Required]
        [StringLength(Common.Const.EmailOrPhoneLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        [Display(Name = "User ID")]
        [EmailAddress]
        public string EmailOrPhone { get; set; }
    }

    public class UserRegistrationCodeVerificationViewModel
    {
        [Required]
        [StringLength(Common.Const.EmailOrPhoneLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        [Display(Name = "User ID")]
        [EmailAddress]
        public string EmailOrPhone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Common.Const.VerificationCodeLength , ErrorMessage = "Verification Code is 12 character length", MinimumLength = Common.Const.VerificationCodeLength)]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }
    }

    public class UserLogginViewModel
    {
        [Required]
        [StringLength(Common.Const.EmailOrPhoneLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        [Display(Name = "User ID")]
        [EmailAddress]
        public string EmailOrPhone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Common.Const.PasswordLength_Max, ErrorMessage = "Password length is between 8 - 16", MinimumLength = Common.Const.PasswordLength_Min)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class UserInfoCompletionViewModel
    {
        [Required]
        [StringLength(Common.Const.EmailOrPhoneLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        [Display(Name = "User ID")]
        public string EmailOrPhone { get; set; }

        [Required]
        [Display(Name = "Nickname")]
        [StringLength(Common.Const.NicknameLength_Max, ErrorMessage = "Nickname should not exceed 32 characters.", MinimumLength = Common.Const.NicknameLength_Min)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(Common.Const.PasswordLength_Max, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Common.Const.PasswordLength_Min)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}