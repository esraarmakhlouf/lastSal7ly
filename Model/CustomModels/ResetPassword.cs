using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class ResetPasswordEmail
    {
        [Remote("ValidateEmail", "Account", ErrorMessage = "No Email Found Match Entered Email.")]
        [Required(ErrorMessage = "Email Required")]
       /// [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
       //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
    public class ResetPassword
    {

        [Required(ErrorMessage = "Password Required")]
        [StringLength(50, ErrorMessage = "Must be at max {1} characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Must be Matched")]
        public string ConfirmPassword { get; set; }
        public Guid token { get; set; }
        public bool ActiveToken { get; set; } = true;
    }
}
